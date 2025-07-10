using HolidayApi.Configurations.Database;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly ApplicationDbContext _context;

        public MunicipalityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> FindMunicipalityIdAsync(int ibgeCode)
        {
            return await _context.Municipality
                            .Where(m => m.IbgeCode == ibgeCode)
                            .Select(m => m.Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveMunicipality(MunicipalityDto municipality)
        {
            try
            {
                Municipality data = new Municipality
                {
                    Name = municipality.Name,
                    IbgeCode = municipality.IbgeCode,
                    StateId = municipality.StateId
                };

                await _context.Municipality.AddAsync(data);
                await _context.SaveChangesAsync();

                return data.Id;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }

        }
    }
}