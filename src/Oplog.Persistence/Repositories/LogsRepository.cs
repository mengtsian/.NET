using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        public readonly OplogDbContext _dbContext;
        public LogsRepository(OplogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Log> Get(int Id)
        {
            return await _dbContext.Logs.SingleOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<List<Log>> GetByIds(List<int> ids)
        {
            return await _dbContext.Logs.Where(l => ids.Contains(l.Id)).ToListAsync();
        }
        public async Task Insert(Log log)
        {
            await _dbContext.Logs.AddAsync(log);
        }
        public async Task Delete(int id)
        {
            var log = await _dbContext.Logs.SingleOrDefaultAsync(l => l.Id == id);

            if (log != null)
            {
                _dbContext.Logs.Remove(log);
            }
        }

        public void DeleteBulk(IEnumerable<Log> logs)
        {
            _dbContext.Logs.RemoveRange(logs);
        }

        public void Update(Log log)
        {
            _dbContext.Logs.Update(log);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<LogsView>> GetAll()
        {
            return await _dbContext.LogsView.ToListAsync();
        }

        public async Task<List<LogsView>> GetLogsBydate(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.LogsView.Where(l => l.CreatedDate >= fromDate && l.CreatedDate <= toDate).OrderByDescending(l => l.CreatedDate).Take(1000).ToListAsync();
        }

        public async Task<List<LogsView>> GetFilteredLogs(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate, string sortField, string sortDirection)
        {
            var fromDateParam = new SqlParameter("@FromDate", fromDate);
            var toDateParam = new SqlParameter("@ToDate", toDate);

            var searchTextParam = new SqlParameter("@SearchText", searchText ?? (object)DBNull.Value);
            var logTypeIdsParam = new SqlParameter("@LogTypeIds", SqlDbType.Structured)
            {
                Value = ConvertFromAnArray(logTypeIds),
                TypeName = "dbo.Ids"
            };

            var areaIdsParam = new SqlParameter("@AreaIds", SqlDbType.Structured)
            {
                Value = ConvertFromAnArray(areaIds),
                TypeName = "dbo.Ids"
            };

            var subTypeIdsParam = new SqlParameter("@SubTypeIds", SqlDbType.Structured)
            {
                Value = ConvertFromAnArray(subTypeIds),
                TypeName = "dbo.Ids"
            };

            var unitIdsParam = new SqlParameter("@UnitIds", SqlDbType.Structured)
            {
                Value = ConvertFromAnArray(unitIds),
                TypeName = "dbo.Ids"
            };

            var sortFieldParam = new SqlParameter("@SortField", sortField ?? (object)DBNull.Value);
            var sortDirectionParam = new SqlParameter("@SortDirection", sortDirection ?? (object)DBNull.Value);
            return await _dbContext.LogsView.FromSqlRaw
                ("GetFilteredLogs @FromDate, @ToDate, @SearchText, @LogTypeIds, @AreaIds, @SubTypeIds, @UnitIds, @SortField, @SortDirection",
                    fromDateParam, toDateParam, searchTextParam, logTypeIdsParam, areaIdsParam, subTypeIdsParam, unitIdsParam, sortFieldParam, sortDirectionParam)
                    .ToListAsync();
        }

        public async Task<LogsView> GetDetailedLogById(int Id)
        {
            return await _dbContext.LogsView.SingleOrDefaultAsync(l => l.Id == Id);
        }


        //TODO: Move to Utils
        private static DataTable ConvertFromAnArray(int[] ids)
        {
            DataTable dt = new();
            dt.Columns.Add("Id", typeof(int));

            if (ids != null)
            {
                foreach (int id in ids)
                {
                    dt.Rows.Add(id);
                }
            }

            return dt;
        }
    }
}
