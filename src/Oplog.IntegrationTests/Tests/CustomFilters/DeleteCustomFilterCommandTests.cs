using NUnit.Framework;
using Oplog.Core.Commands.CustomFilters;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.CustomFilters;

public class DeleteCustomFilterCommandTests : TestBase
{
    [Test]
    public async Task DisPatch_ShouldDeleteCustomFilter()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var createCustomFilterCommand = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", false, "Test", false, filterItems);
        var createResult = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommand);

        var deleteCustomFilterCommand = new DeleteCustomFilterCommand(createResult.FilterId, IsAdmin: false);
        var deleteResult = await CommandDispatcher.Dispatch<DeleteCustomFilterCommand, DeleteCustomFilterResult>(deleteCustomFilterCommand);

        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.Success);
    }

    [Test]
    public async Task DisPatch_ShouldNotDeleteGlobalCustomFilter()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var createCustomFilterCommand = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", true, "Test", true, filterItems);
        var createResult = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommand);

        var deleteCustomFilterCommand = new DeleteCustomFilterCommand(createResult.FilterId, IsAdmin: false);
        var deleteResult = await CommandDispatcher.Dispatch<DeleteCustomFilterCommand, DeleteCustomFilterResult>(deleteCustomFilterCommand);

        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.NotAllowed);
    }

    [Test]
    public async Task DisPatch_ShouldDeleteGlobalCustomFilterAsAdmin()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var createCustomFilterCommand = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", true, "Test", true, filterItems);
        var createResult = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommand);

        var deleteCustomFilterCommand = new DeleteCustomFilterCommand(createResult.FilterId, IsAdmin: true);
        var deleteResult = await CommandDispatcher.Dispatch<DeleteCustomFilterCommand, DeleteCustomFilterResult>(deleteCustomFilterCommand);

        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.Success);
    }
}
