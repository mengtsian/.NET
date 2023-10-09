using NUnit.Framework;
using Oplog.Core.Commands.CustomFilters;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.CustomFilters;

public class CreateCustomFilterCommandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldCreateCustomFilter()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var createCustomFilterCommad = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", false, "Test", false, filterItems);

        var result = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommad);
        Assert.IsTrue(result.ResultType == ResultTypeConstants.Success);
    }

    [Test]
    public async Task Dispatch_ShouldCreateGlobalCustomFilterAsAdmin()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var isAdmin = true;
        var isGlobalFilter = true;
        var createCustomFilterCommad = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", isGlobalFilter, "Test", isAdmin, filterItems);

        var result = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommad);
        Assert.IsTrue(result.ResultType == ResultTypeConstants.Success);
    }

    [Test]
    public async Task Dispatch_ShouldNotCreateGlobalCustomFilterAsNonAdmin()
    {
        var filterItems = new List<CreateCustomFilterItem>
        {
            new CreateCustomFilterItem() { CategoryId = 2, FilterId = 3 },
            new CreateCustomFilterItem() { CategoryId = 4, FilterId = 5 }
        };

        var isAdmin = false;
        var isGlobalFilter = true;
        var createCustomFilterCommad = new CreateCustomFilterCommand("Test Filter", "bonm@equinor.com", isGlobalFilter, "Test", isAdmin, filterItems);

        var result = await CommandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(createCustomFilterCommad);
        Assert.IsTrue(result.ResultType == ResultTypeConstants.NotAllowed);
    }
}
