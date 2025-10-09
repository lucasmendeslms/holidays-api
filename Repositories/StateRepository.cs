using HolidayApi.Configurations.Database;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Repositories
{
    public class StateRepository : IStateRepository
    {
        private const int ONE_ROW_AFFECTED = 1;
        private const int NO_ROWS_AFFECTED = 0;
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

            await _context.State.AddAsync(data);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows == ONE_ROW_AFFECTED ? data.Id : NO_ROWS_AFFECTED;
        }
    }
}