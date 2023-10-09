using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ICustomFilterRepository
    {
        Task<CustomFilter> GetById(int id);
        Task Insert(CustomFilter filter);
        Task<List<CustomFilter>> GetByCreatedUser(string createdBy);
        Task<List<CustomFilter>> GetGlobalCustomFilters();
        void Delete(CustomFilter customFilter);
        Task Save();
    }
}