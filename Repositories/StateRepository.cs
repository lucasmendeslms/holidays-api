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

        public async Task<State?> FindStateAsync(int ibgeCode)
        {
            return await _context.State
                            .Where(s => s.IbgeCode == ibgeCode)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveState(StateDto state)
        {
            try
            {
                State data = new State
                {
                    Name = state.Name,
                    IbgeCode = state.IbgeCode,
                    StateCode = state.StateCode
                };

                await _context.State.AddAsync(data);
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