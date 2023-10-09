namespace Oplog.Core.Queries;

public interface ILogTemplateQueries
{
    Task<List<GetAllLogTemplateResult>> GetAll();
}