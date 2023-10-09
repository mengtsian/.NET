using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.Logs;

public class CreateLogCommandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldCreateLog()
    {
        var createLogCommand = new CreateLogCommand(LogType: 415, SubType: 1079, Comment: "Test comment", OperationsAreaId: 10000, Author: "Donald Trump", Unit: 1086, EffectiveTime: DateTime.Now, CreatedBy: "bonm@equinor.com", IsCritical: false);
        var result = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);

        Assert.IsTrue(result.ResultType == ResultTypeConstants.Success);
    }
}
