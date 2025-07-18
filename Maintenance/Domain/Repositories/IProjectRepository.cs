using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;
using eb4341u202318323.API.Shared.Domain.Repositories;

namespace eb4341u202318323.API.Maintenance.Domain.Repositories;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<IEnumerable<Project>> FindByCodeProject(CodeProject codeProject);
    Task<bool> ExistsByCodeProjectAndProjectName(CodeProject codeProject, string projectName);
}