using NUnit.Framework;
using Oplog.Core.Commands.LogTemplates;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.LogTemplates;

public class DeleteLogTemplateCommandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldDeleteLogTemplate()
    {
        var createLogTemplateCommand = new CreateLogTemplateCommand("Test template", null, null, null, "test template", null, null, null, "bonm@equinor.com");
        var result = await CommandDispatcher.Dispatch<CreateLogTemplateCommand, CreateLogTemplateResult>(createLogTemplateCommand);

        var deleteResult = await CommandDispatcher.Dispatch<DeleteLogTemplateCommand, DeleteLogTemplateResult>(new DeleteLogTemplateCommand(result.LogTemplateId));
        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.Success);
    }

    [Test]
    public async Task Dispatch_ShouldReturnNotFound()
    {
        var logTemplateId = 10;
        var deleteResult = await CommandDispatcher.Dispatch<DeleteLogTemplateCommand, DeleteLogTemplateResult>(new DeleteLogTemplateCommand(logTemplateId));
        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.NotFound);
    }
}
