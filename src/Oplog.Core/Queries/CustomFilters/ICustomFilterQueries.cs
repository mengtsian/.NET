namespace Oplog.Core.Queries;

public interface ICustomFilterQueries
{
    Task<List<GetCustomFiltersByCreatedUserResult>> GetByCreatedUser(string createdBy);
    Task<List<GetGlobalCustomFiltersResult>> GetGlobalCustomFilters();
}