using HolidayApi.Configurations.Database;
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

        //Municipality

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

        public async Task<Holiday?> FindMunicipalityHoliday(int ibgeCode, HolidayDate date)
        {
            var holiday = await _context.Holiday
                .Where(
                    h => h.Day == date.Date.Day &&
                    h.Month == date.Date.Month &&
                    h.Municipality != null &&
                    h.Municipality.IbgeCode == ibgeCode)
                .FirstOrDefaultAsync();

            return holiday;
        }

        //State

        public async Task<IEnumerable<HolidayDto>> FindAllStateHolidays(int ibgeCode)
        {
            var holidays = await _context.Holiday
                .Where(h => h.State != null && h.State.IbgeCode == ibgeCode)
                .Select(h => new HolidayDto
                {
                    Name = h.Name
                })
                .ToListAsync();

            return holidays.AsEnumerable();
        }

        public async Task<Holiday?> FindStateHoliday(int ibgeCode, HolidayDate date)
        {
            return await _context.Holiday
                    .Where(h =>
                        h.Day == date.Date.Day &&
                        h.Month == date.Date.Month &&
                        h.State != null &&
                        h.State.IbgeCode == ibgeCode)
                    .FirstOrDefaultAsync();
        }

        public async Task<int> SaveStateHoliday(int stateId, HolidayDate date, string name)
        {
            try
            {
                Holiday holiday = new Holiday
                {
                    Name = name,
                    Day = date.Date.Day,
                    Month = date.Date.Month,
                    Year = date.Date.Year,
                    Type = HolidayType.State,
                    StateId = stateId
                };

                await _context.Holiday.AddAsync(holiday);
                await _context.SaveChangesAsync();

                return StatusCodes.Status201Created;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to save a new holiday | SaveStateHoliday | {e.Message}");
            }
        }

        public async Task<int> SaveMunicipalityHoliday(int municipalityId, HolidayDate date, string name)
        {
            try
            {
                Holiday holiday = new Holiday
                {
                    Name = name,
                    Day = date.Date.Day,
                    Month = date.Date.Month,
                    Year = date.Date.Year,
                    Type = HolidayType.Municipal,
                    MunicipalityId = municipalityId
                };

                await _context.Holiday.AddAsync(holiday);
                await _context.SaveChangesAsync();

                return StatusCodes.Status201Created;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to save a new holiday | SaveMunicipalityHoliday | {e.Message}");
            }
        }
    }
}