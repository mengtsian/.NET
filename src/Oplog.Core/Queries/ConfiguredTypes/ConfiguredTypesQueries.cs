using Microsoft.Extensions.Caching.Memory;
using Oplog.Core.Common;
using Oplog.Core.Queries.ConfiguredTypes;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries;

public class ConfiguredTypesQueries : IConfiguredTypesQueries
{
    private readonly IConfiguredTypesRepository _configuredTypesRepository;
    private readonly IMemoryCache _memoryCache;

    public ConfiguredTypesQueries(IConfiguredTypesRepository configuredTypesRepository, IMemoryCache memoryCache)
    {
        _configuredTypesRepository = configuredTypesRepository;
        _memoryCache = memoryCache;
    }

    public async Task<List<ConfiguredTypesByCategoryResult>> GetConfiguredTypesByCategory(CategoryId categoryId)
    {
        var configuredTypes = await _configuredTypesRepository.GetByCategory((int)categoryId);

        if (configuredTypes == null)
        {
            return null;
        }

        var results = new List<ConfiguredTypesByCategoryResult>();
        foreach (var type in configuredTypes)
        {
            results.Add(new ConfiguredTypesByCategoryResult(type.Id, type.Name, type.Description, type.CategoryId));
        }

        return results;
    }

    public async Task<AllConfiguredTypesResultGrouped> GetAllGrouped()
    {
        var configuredTypes = await _configuredTypesRepository.GetAll();

        var types = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Type).OrderBy(c => c.Name);
        var subTypes = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.SubType).OrderBy(c => c.Name);
        var units = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Unit).OrderBy(c => c.Name);

        var result = new AllConfiguredTypesResultGrouped();
        foreach (var type in types)
        {
            result.Types.Add(new ConfiguredTypeResult(type.Id, type.Name, type.Description, type.CategoryId));
        }
        foreach (var subType in subTypes)
        {
            result.SubTypes.Add(new ConfiguredTypeResult(subType.Id, subType.Name, subType.Description, subType.CategoryId));
        }
        foreach (var unit in units)
        {
            result.Units.Add(new ConfiguredTypeResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
        }

        return result;
    }

    public async Task<AllConfiguredTypesResultGrouped> GetGroupedActiveConfiguredTypes()
    {
        var result = await _memoryCache.GetOrCreateAsync("active_types_grouped", async cacheEntry =>
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            cacheEntry.SetOptions(cacheEntryOptions);

            var data = new AllConfiguredTypesResultGrouped();
            var configuredTypes = await _configuredTypesRepository.GetAllActive();

            var types = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Type).OrderBy(c => c.Name);
            var subTypes = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.SubType);
            var units = configuredTypes.Where(c => c.CategoryId == (int)CategoryId.Unit).OrderBy(c => c.Name);


            foreach (var type in types)
            {
                data.Types.Add(new ConfiguredTypeResult(type.Id, type.Name, type.Description, type.CategoryId));
            }
            foreach (var subType in subTypes)
            {
                data.SubTypes.Add(new ConfiguredTypeResult(subType.Id, subType.Name, subType.Description, subType.CategoryId));
            }
            foreach (var unit in units)
            {
                data.Units.Add(new ConfiguredTypeResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
            }

            return data;

        });

        return result;
    }
}
