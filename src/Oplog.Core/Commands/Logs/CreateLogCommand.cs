using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.Logs;

public sealed record CreateLogCommand(int LogType, int SubType, string Comment, int OperationsAreaId, string Author, int Unit, DateTime EffectiveTime, string CreatedBy, bool? IsCritical) : ICommand;
