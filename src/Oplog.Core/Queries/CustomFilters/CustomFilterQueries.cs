using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries;

public class CustomFilterQueries : ICustomFilterQueries
{
    private readonly ICustomFilterRepository _customFilterRepository;
    private readonly IOperationsAreasRepository _operationsAreasRepository;
    private readonly IConfiguredTypesRepository _configuredTypesRepository;
    public CustomFilterQueries(ICustomFilterRepository customFilterRepository, IOperationsAreasRepository operationsAreasRepository, IConfiguredTypesRepository configuredTypesRepository)
    {
        _customFilterRepository = customFilterRepository;
        _operationsAreasRepository = operationsAreasRepository;
        _configuredTypesRepository = configuredTypesRepository;
    }

    public async Task<List<GetCustomFiltersByCreatedUserResult>> GetByCreatedUser(string createdBy)
    {
        var customFilters = await _customFilterRepository.GetByCreatedUser(createdBy);

        if (customFilters == null)
        {
            return null;
        }

        var results = new List<GetCustomFiltersByCreatedUserResult>();

        foreach (var item in customFilters)
        {
            var result = new GetCustomFiltersByCreatedUserResult
            {
                Id = item.Id,
                Name = item.Name,
                SearchText = item.SearchText,
                Filters = new List<CustomFilterItemsResult>()
            };

            foreach (var filterItem in item.CustomFilterItems)
            {
                string filterName;
                if (filterItem.CategoryId == null)
                {
                    filterName = await GetAreaNameById(filterItem.FilterId);
                }
                else
                {
                    filterName = await GetConfiguredTypeNameById(filterItem.FilterId);
                }

                result.Filters.Add(new CustomFilterItemsResult { Id = filterItem.FilterId, CategoryId = filterItem.CategoryId, Name = filterName });
            }

            results.Add(result);
        }

        return results;
    }

    public async Task<List<GetGlobalCustomFiltersResult>> GetGlobalCustomFilters()
    {
        var customFilters = await _customFilterRepository.GetGlobalCustomFilters();

        if (customFilters == null)
        {
            return null;
        }

        var results = new List<GetGlobalCustomFiltersResult>();

        foreach (var item in customFilters)
        {
            var result = new GetGlobalCustomFiltersResult
            {
                Id = item.Id,
                Name = item.Name,
                IsGlobalFilter = item.IsGlobalFilter,
                SearchText = item.SearchText,
                Filters = new List<CustomFilterItemsResult>()
            };

            foreach (var filterItem in item.CustomFilterItems)
            {
                string filterName;
                if (filterItem.CategoryId == null)
                {
                    filterName = await GetAreaNameById(filterItem.FilterId);
                }
                else
                {
                    filterName = await GetConfiguredTypeNameById(filterItem.FilterId);
                }

                result.Filters.Add(new CustomFilterItemsResult { Id = filterItem.FilterId, CategoryId = filterItem.CategoryId, Name = filterName });
            }

            results.Add(result);
        }

        results.Sort((a, b) => string.Compare(a.Name, b.Name));

        return results;
    }
    private async Task<string> GetAreaNameById(int id)
    {
        var area = await _operationsAreasRepository.Get(id);
        if (area == null)
        {
            return null;
        }

        return area.Name;
    }

    private async Task<string> GetConfiguredTypeNameById(int id)
    {
        var configuredType = await _configuredTypesRepository.Get(id);

        if (configuredType == null)
        {
            return null;
        }

        return configuredType.Name;
    }
}
