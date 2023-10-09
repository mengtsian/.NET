using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.Logs;

public class UpdateLogCommandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldUpdateLog()
    {
        var createLogCommand = new CreateLogCommand(LogType: 415, SubType: 1079, Comment: "Test comment", OperationsAreaId: 10000, Author: "Bonyfus Martin", Unit: 1086, EffectiveTime: DateTime.Now, CreatedBy: "bonm@equinor.com", IsCritical: false);
        var createResult = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommand);

        var updateCommand = new UpdateLogCommand(createResult.LogId, LogType: 415, SubType: 1079, Comment: "Update comment", OperationsAreaId: 10000, Author: "Donald Trump", Unit: 1086, EffectiveTime: DateTime.Now, UpdatedBy: "bonm@equinor.com", IsCritical: false);
        var updateResult = await CommandDispatcher.Dispatch<UpdateLogCommand, UpdateLogResult>(updateCommand);

        Assert.IsTrue(updateResult.ResultType == ResultTypeConstants.Success);
    }

    [Test]
    public async Task Dispatch_ShouldReturnLogNotFound()
    {
        var updateCommand = new UpdateLogCommand(10, LogType: 415, SubType: 1079, Comment: "Update comment", OperationsAreaId: 10000, Author: "Donald Trump", Unit: 1086, EffectiveTime: DateTime.Now, UpdatedBy: "bonm@equinor.com", IsCritical: false);
        var updateResult = await CommandDispatcher.Dispatch<UpdateLogCommand, UpdateLogResult>(updateCommand);

        Assert.IsTrue(updateResult.ResultType == ResultTypeConstants.NotFound);
    }
}
