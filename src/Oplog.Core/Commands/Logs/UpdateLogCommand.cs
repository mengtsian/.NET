using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.Logs;

public sealed record UpdateLogCommand(int Id, int LogType, int SubType, string Comment, int OperationsAreaId, string Author, int Unit, DateTime EffectiveTime, string UpdatedBy, bool? IsCritical) : ICommand;

