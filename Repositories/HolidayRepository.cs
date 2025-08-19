using HolidayApi.Configurations.Database;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ResponseHandler;
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

                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public async Task<int> DeleteHolidayById(int id)
        {
            return await _context.Holiday
                .Where(h => h.Id == id)
                .ExecuteDeleteAsync();
        }

        //Municipality

        public async Task<IEnumerable<HolidayDetailDto>> FindAllMunicipalityHolidays(int ibgeCode)
        {
            var holidays = await _context.Holiday
                .Join(
                    _context.Municipality,
                    holiday => holiday.MunicipalityId,
                    municipality => municipality.Id,
                    (holiday, municipality) => new { holiday, municipality }
                )
                .Where(municipalHoliday => municipalHoliday.municipality.IbgeCode == ibgeCode)
                .Select(municipalHoliday => new HolidayDetailDto(
                    municipalHoliday.holiday.Name,
                    municipalHoliday.holiday.Month,
                    municipalHoliday.holiday.Day,
                    municipalHoliday.holiday.Type
                ))
                .ToListAsync();

            return holidays;
        }

        public async Task<Holiday?> FindMunicipalityHoliday(int ibgeCode, HolidayDate date)
        {
            return await _context.Holiday
                .Where(
                    h => h.Day == date.Date.Day &&
                    h.Month == date.Date.Month &&
                    h.Municipality != null &&
                    h.Municipality.IbgeCode == ibgeCode)
                .FirstOrDefaultAsync();
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

        //State

        public async Task<IEnumerable<HolidayDetailDto>> FindAllStateHolidays(int ibgeCode)
        {
            return await _context.Holiday
                .Join(
                    _context.State,
                    holiday => holiday.StateId,
                    state => state.Id,
                    (holiday, state) => new { holiday, state }
                )
                .Where(stateHoliday => stateHoliday.state.IbgeCode == ibgeCode)
                .Select(stateHoliday => new HolidayDetailDto(
                    stateHoliday.holiday.Name,
                    stateHoliday.holiday.Month,
                    stateHoliday.holiday.Day,
                    stateHoliday.holiday.Type
                ))
                .ToListAsync();
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
            Holiday holiday = new Holiday
            {
                Name = name,
                Day = date.Date.Day,
                Month = date.Date.Month,
                Type = HolidayType.State,
                StateId = stateId
            };

            await _context.Holiday.AddAsync(holiday);
            return await _context.SaveChangesAsync();
        }
    }
}