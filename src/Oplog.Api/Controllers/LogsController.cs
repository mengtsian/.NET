using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.AzureSearch;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Common;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using Oplog.Core.Queries.Logs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/logs")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogsQueries _queries;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISearchLogsQueries _searchLogsQueries;
        public LogsController(ICommandDispatcher commandDispatcher, ILogsQueries queries, ISearchLogsQueries searchLogsQueries, IHttpContextAccessor contextAccessor)
        {
            _commandDispatcher = commandDispatcher;
            _queries = queries;
            _searchLogsQueries = searchLogsQueries;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _queries.GetAllLogs();
            return Ok(result);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Post(GetFilteredLogsRequest request)
        {
            var filter = new LogsFilter(request.LogTypeIds, request.AreaIds, request.SubTypeIds, request.UnitIds, request.SearchText, request.FromDate.Value, request.ToDate.Value, request.SortField, request.SortDirection);
            var result = await _queries.GetFilteredLogs(filter);
            return Ok(result);
        }

        [HttpGet("{fromDate}/{toDate}")]
        public async Task<IActionResult> Get(DateTime fromDate, DateTime toDate)
        {
            var result = await _queries.GetLogsByDate(fromDate, toDate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLogRequest request)
        {
            var result = await _commandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(new CreateLogCommand(request.LogType.Value, request.SubType, request.Comment, request.OperationsAreaId.Value, GetFullName(), request.Unit.Value, request.EffectiveTime.Value, GetUserName(), request.IsCritical));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLogRequest request)
        {
            var result = await _commandDispatcher.Dispatch<UpdateLogCommand, UpdateLogResult>(new UpdateLogCommand(id, request.LogType.Value, request.SubType, request.Comment, request.OperationsAreaId.Value, request.Author, request.Unit.Value, request.EffectiveTime.Value, GetUserName(), request.IsCritical));
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] GetSearchLogsRequest request)
        {
            SearchLogsResult result = await _searchLogsQueries.Search(new SearchRequest(request.LogTypeIds, request.AreaIds, request.SubTypeIds, request.UnitIds, request.SearchText, request.FromDate, request.ToDate, request.SortBy, request.PageSize, request.PageNumber));
            return Ok(result);
        }

        [HttpPost("search-by-ids")]
        public async Task<IActionResult> Search([FromBody] GetSearchLogsByIdsRequest request)
        {
            GetLogsByIdsSearchResult result = await _searchLogsQueries.GetLogsByIds(request.LogIds, request.SortBy);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(IEnumerable<int> ids)
        {
            var result = await _commandDispatcher.Dispatch<DeleteLogsCommand, DeleteLogsResult>(new DeleteLogsCommand(ids));            
            return Ok(result);
        }

        private string GetFullName()
        {
            var givenName = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            var surname = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);
            return $"{givenName} {surname}";
        }

        private string GetUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }

    }
}
