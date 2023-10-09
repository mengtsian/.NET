using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.LogTemplates;

public sealed record DeleteLogTemplateCommand : ICommand
{
    public DeleteLogTemplateCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}
