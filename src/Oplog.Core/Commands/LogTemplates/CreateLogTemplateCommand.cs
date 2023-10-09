using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.LogTemplates;

public sealed record CreateLogTemplateCommand(string Name, int? LogTypeId, int? AreaId, string Text, string Author, int? Unit, int? SubType, bool? IsCritical, string CreatedBy) : ICommand;
