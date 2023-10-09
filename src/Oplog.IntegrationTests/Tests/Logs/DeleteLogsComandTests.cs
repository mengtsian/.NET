using NUnit.Framework;
using Oplog.Core.Commands.Logs;
using Oplog.Core.Common;

namespace Oplog.IntegrationTests.Tests.Logs;

public class DeleteLogsComandTests : TestBase
{
    [Test]
    public async Task Dispatch_ShouldDeleteAllLogs()
    {
        var createLogCommandA = new CreateLogCommand(LogType: 415, SubType: 1079, Comment: "Test comment", OperationsAreaId: 10000, Author: "Bonyfus Martin", Unit: 1086, EffectiveTime: DateTime.Now, CreatedBy: "bonm@equinor.com", IsCritical: false);
        var createResultA = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommandA);

        var createLogCommandB = new CreateLogCommand(LogType: 415, SubType: 1079, Comment: "Test comment", OperationsAreaId: 10000, Author: "Bonyfus Martin", Unit: 1086, EffectiveTime: DateTime.Now, CreatedBy: "bonm@equinor.com", IsCritical: false);
        var createResultB = await CommandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(createLogCommandB);

        var ids = new List<int>
        {
            createResultA.LogId,
            createResultB.LogId
        };

        var deleteResult = await CommandDispatcher.Dispatch<DeleteLogsCommand, DeleteLogsResult>(new DeleteLogsCommand(ids));

        Assert.IsTrue(deleteResult.ResultType == ResultTypeConstants.Success);
    }    
}
