using NUnit.Framework;
using Oplog.Core.Commands.LogTemplates;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.LogTemplates;

public class CreateLogTemplateCommandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldCreateLogTemplate()
    {
        var createLogTemplateCommand = new CreateLogTemplateCommand("Test template", null, null, null, "test template", null, null, null, "bonm@equinor.com");
        var result = await CommandDispatcher.Dispatch<CreateLogTemplateCommand, CreateLogTemplateResult>(createLogTemplateCommand);
        Assert.IsTrue(result.ResultType == ResultTypeConstants.Success);
    }
}
