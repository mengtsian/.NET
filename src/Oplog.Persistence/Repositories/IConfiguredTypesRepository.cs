using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface IConfiguredTypesRepository
    {
        Task<List<ConfiguredType>> GetByCategory(int categoryId);
        Task<ConfiguredType> Get(int id);
        Task<List<ConfiguredType>> GetAll();
        Task<List<ConfiguredType>> GetAllActive();
    }
}
