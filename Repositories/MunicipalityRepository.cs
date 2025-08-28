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
            Municipality data = municipality;

            await _context.Municipality.AddAsync(data);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows == 1 ? data.Id : 0;
        }
    }
}