using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands.CustomFilters;
using Oplog.Core.Common;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/customfilter")]
    [ApiController]
    [Authorize]
    public class CustomFilterController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICustomFilterQueries _customFilterQueries;
        private readonly IHttpContextAccessor _contextAccessor;
        public CustomFilterController(ICommandDispatcher commandDispatcher, ICustomFilterQueries customFilterQueries, IHttpContextAccessor contextAccessor)
        {
            _commandDispatcher = commandDispatcher;
            _customFilterQueries = customFilterQueries;
            _contextAccessor = contextAccessor;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomFilterRequest request)
        {
            var isAdmin = _contextAccessor.HttpContext.User.IsInRole("Permission.Admin");
            var result = await _commandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(new CreateCustomFilterCommand(request.Name, GetUserName(), request.IsGlobalFilter, request.SearchText, isAdmin, request.FilterItems));

            if (result.ResultType == ResultTypeConstants.NotAllowed)
            {
                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.SetString("message", result.Message);
                return Forbid(authenticationProperties);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _customFilterQueries.GetByCreatedUser(GetUserName());
            return Ok(results);
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalCustomFilters()
        {
            var results = await _customFilterQueries.GetGlobalCustomFilters();
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isAdmin = _contextAccessor.HttpContext.User.IsInRole("Permission.Admin");
            var result = await _commandDispatcher.Dispatch<DeleteCustomFilterCommand, DeleteCustomFilterResult>(new DeleteCustomFilterCommand(id, isAdmin));

            if (result.ResultType == ResultTypeConstants.NotFound)
            {
                return NotFound(result);
            }

            if (result.ResultType == ResultTypeConstants.NotAllowed)
            {
                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.SetString("message", result.Message);
                return Forbid(authenticationProperties);
            }

            return Ok(result);
        }

        private string GetUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
