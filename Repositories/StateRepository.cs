using HolidayApi.Configurations.Database;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _context;

        public StateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> FindStateIdAsync(int ibgeCode)
        {
            return await _context.State
                            .Where(s => s.IbgeCode == ibgeCode)
                            .Select(s => s.Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveState(StateDto state)
        {
            State data = state;

            try
            {
                await _context.State.AddAsync(data);

                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows == 1 ? data.Id : 0;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message); ;
            }

        }
    }
}