using Azure.Search.Documents;
using Oplog.Core.Utils;
using System.Text;

namespace Oplog.Core.AzureSearch;

public class SearchOptionsBuilder
{
    private readonly SearchOptions _searchOptions = new();
    private readonly StringBuilder _filter = new();
    private StringBuilder _fieldsFilter = new();
    private const string DateTimeFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
    private readonly bool _isDateOnlySearch = false;
    private const string FilterPlaceHolderValue = "[]";
    private const string LogTypeIdFieldName = "LogTypeId", AreaIdFieldName = "OperationAreaId", SubTypeIdFieldName = "Subtype", UnitIdFieldName = "Unit";

    public SearchOptionsBuilder(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, bool isDateOnlySearch)
    {
        _isDateOnlySearch = isDateOnlySearch;
        _searchOptions.IncludeTotalCount = true;
        _searchOptions.Size = pageSize;

        if (pageNumber > 0)
        {
            _searchOptions.Skip = (pageNumber - 1) * pageSize;
        }

        if (_isDateOnlySearch)
        {
            _filter.Append($"EffectiveTime ge {fromDate.ToString(DateTimeFormat)} and EffectiveTime le {toDate.ToString(DateTimeFormat)}");
        }
        else
        {
            _filter.Append($"(EffectiveTime ge {fromDate.ToString(DateTimeFormat)} and EffectiveTime le {toDate.ToString(DateTimeFormat)}) and ({FilterPlaceHolderValue})");
        }
    }

    public void AddSortFields(List<string> sortFields)
    {
        if (sortFields == null)
        {
            _searchOptions.OrderBy.Add("EffectiveTime desc");
            return;
        }
        else
        {
            foreach (var fieldWithDirection in sortFields)
            {
                _searchOptions.OrderBy.Add(StringUtils.ConvertFirstLetterToUpper(fieldWithDirection));
            }
        }
    }

    public void AddSearchTextFilter(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) return;

        _fieldsFilter.Append($" and search.ismatch('{searchText.ToLower()}*', 'Text')");
    }

    public void AddLogTypeFilter(int[] logTypeIds)
    {
        if (logTypeIds == null || logTypeIds.Length == 0) return;

        string logTypeFilter = CreateFieldsFilter(logTypeIds, LogTypeIdFieldName);
        _fieldsFilter.Append($" and ({logTypeFilter})");
    }

    public void AddAreaFilter(int[] areaIds)
    {
        if (areaIds == null || areaIds.Length == 0) return;

        string areaFilter = CreateFieldsFilter(areaIds, AreaIdFieldName);
        _fieldsFilter.Append($" and ({areaFilter})");
    }

    public void AddSubTypeFilter(int[] subTypeIds)
    {
        if (subTypeIds == null || subTypeIds.Length == 0) return;

        string subTypeFilter = CreateFieldsFilter(subTypeIds, SubTypeIdFieldName);
        _fieldsFilter.Append($" and ({subTypeFilter})");
    }

    public void AddUnitFilter(int[] unitIds)
    {
        if (unitIds == null || unitIds.Length == 0) return;

        string unitFilter = CreateFieldsFilter(unitIds, UnitIdFieldName);
        _fieldsFilter.Append($" and ({unitFilter})");
    }

    public SearchOptions Build()
    {
        if (_isDateOnlySearch)
        {
            _searchOptions.Filter = _filter.ToString();
        }
        else
        {
            if (_fieldsFilter.Length != 0)
            {
                //Note: remove the "and" operator
                _fieldsFilter = _fieldsFilter.Remove(0, 5);

                string filter = _filter.ToString();
                filter = filter.Replace("[]", _fieldsFilter.ToString());
                _searchOptions.Filter = filter;
            }
        }

        return _searchOptions;
    }

    private static string CreateFieldsFilter(int[] fieldIds, string fieldName)
    {
        StringBuilder stringBuilder = new();

        foreach (int logTypeId in fieldIds)
        {
            stringBuilder.Append(@$"{fieldName} eq {logTypeId.ToString()} or ");
        }

        string query = stringBuilder.ToString();

        //Note: remove any trailing "or" operators
        query = query.Remove(query.Length - 4);

        return query;
    }
}
