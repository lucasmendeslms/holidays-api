using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;

        public HolidayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HolidayDto>> FindAllMunicipalityHolidays(int ibgeCode)
        {
            var holidays = await _context.Municipality
                .Where(h => h.IbgeCode == ibgeCode)
                .Select(h => new HolidayDto
                {
                    Name = h.Name
                })
                .ToListAsync();

            return holidays.AsEnumerable();
        }

        public async Task<IEnumerable<HolidayDto>> FindAllStateHolidays(int ibgeCode)
        {
            var holidays = await _context.Holiday
                .Where(h => h.States != null && h.States.Any(s => s.IbgeCode == ibgeCode))
                .Select(h => new HolidayDto
                {
                    Name = h.Name
                })
                .ToListAsync();

            return holidays.AsEnumerable();
        }

        public async Task<Holiday?> FindMunicipalityHoliday(int ibgeCode, HolidayDate date)
        {
            var holiday = await _context.Holiday
                .Where(
                    h => h.Day == date.Date.Day &&
                    h.Month == date.Date.Month &&
                    h.Municipalities != null &&
                    h.Municipalities.Any(m => m.IbgeCode == ibgeCode))
                .FirstOrDefaultAsync();

            return holiday;
        }

        public async Task<Holiday?> FindStateHoliday(int ibgeCode, HolidayDate date)
        {
            return await _context.Holiday
                    .Where(h =>
                        h.Day == date.Date.Day &&
                        h.Month == date.Date.Month &&
                        h.States != null &&
                        h.States.Any(s => s.IbgeCode == ibgeCode))
                    .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateHolidayName(int id, string name)
        {
            if (await _context.Holiday.FindAsync(id) is Holiday found)
            {
                found.Name = name;

                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }

            return StatusCodes.Status500InternalServerError;
        }

        public async Task<int> RegisterHoliday(int ibgeCode, DateTime date, string name)
        {

        }
    }
}