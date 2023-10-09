using Microsoft.Extensions.Caching.Memory;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Queries;

public class OperationAreasQueries : IOperationAreasQueries
{
    private readonly IOperationsAreasRepository _areasRepository;
    private readonly IMemoryCache _memoryCache;
    public OperationAreasQueries(IOperationsAreasRepository areasRepository, IMemoryCache memoryCache)
    {
        _areasRepository = areasRepository;
        _memoryCache = memoryCache;
    }
    public async Task<List<GetAllAreasResult>> GetAllAreas()
    {
        var areas = await _areasRepository.GetAllAreas();

        if (areas == null)
        {
            return null;
        }

        var results = new List<GetAllAreasResult>();

        foreach (var area in areas)
        {
            results.Add(new GetAllAreasResult(area.Id, area.Name, area.Description));
        }

        return results;
    }

    public async Task<List<GetAllAreasResult>> GetActiveAreas()
    {
        var result = await _memoryCache.GetOrCreateAsync("active_areas", async cacheEntry =>
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            cacheEntry.SetOptions(cacheEntryOptions);

            var areas = await _areasRepository.GetActiveAreas();
            if (areas == null)
            {
                return null;
            }

            var data = new List<GetAllAreasResult>();

            foreach (var area in areas)
            {
                var units = new List<UnitResult>();
                foreach (var unit in area.Units)
                {
                    units.Add(new UnitResult(unit.Id, unit.Name, unit.Description, unit.CategoryId));
                }

                data.Add(new GetAllAreasResult(area.Id, area.Name, area.Description, units));
            }

            return data;
        });

        return result;
    }
}
