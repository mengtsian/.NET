using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class CustomFilterRepository : ICustomFilterRepository
    {
        public readonly OplogDbContext _dbContext;
        public CustomFilterRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomFilter> GetById(int id)
        {
            return await _dbContext.CustomFilters.Include(c => c.CustomFilterItems).SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task Insert(CustomFilter filter)
        {
            await _dbContext.CustomFilters.AddAsync(filter);
        }

        public async Task<List<CustomFilter>> GetByCreatedUser(string createdBy)
        {
            return await _dbContext.CustomFilters.Where(u => u.CreatedBy == createdBy && u.IsGlobalFilter == false).Include(u => u.CustomFilterItems).ToListAsync();
        }

        public async Task<List<CustomFilter>> GetGlobalCustomFilters()
        {
            return await _dbContext.CustomFilters.Where(c => c.IsGlobalFilter == true).Include(c => c.CustomFilterItems).ToListAsync();
        }

        public void Delete(CustomFilter customFilter)
        {
            if (customFilter != null)
            {
                _dbContext.CustomFilters.Remove(customFilter);
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
