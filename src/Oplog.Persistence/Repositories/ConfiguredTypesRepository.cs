using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class ConfiguredTypesRepository : IConfiguredTypesRepository
    {
        public readonly OplogDbContext _dbContext;

        public ConfiguredTypesRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //TODO: Use cancellation tokens
        public async Task<List<ConfiguredType>> GetByCategory(int categoryId)
        {
            return await _dbContext.ConfiguredTypes.Where(c => c.CategoryId == categoryId && c.IsActive == true).ToListAsync();
        }

        public async Task<ConfiguredType> Get(int id)
        {
            return await _dbContext.ConfiguredTypes.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ConfiguredType>> GetAll()
        {
            return await _dbContext.ConfiguredTypes.ToListAsync();
        }

        public async Task<List<ConfiguredType>> GetAllActive()
        {
            return await _dbContext.ConfiguredTypes.Where(c => c.IsActive == true).ToListAsync();
        }
    }
}
