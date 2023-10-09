using Oplog.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface IOperationsAreasRepository
    {
        Task<List<OperationArea>> GetAllAreas();
        Task<OperationArea> Get(int id);
        Task<List<OperationArea>> GetActiveAreas();
    }
}
