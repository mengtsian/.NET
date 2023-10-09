using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface ILogTemplateRepository
    {
        Task<List<LogTemplate>> GetAll();
        Task<LogTemplate> GetById(int id);
        Task<List<LogTemplate>> GetByUser(string userName);
        Task Insert(LogTemplate template);
        void Update(LogTemplate logTemplate);
        void Delete(LogTemplate logTemplate);
        Task Save();
    }
}