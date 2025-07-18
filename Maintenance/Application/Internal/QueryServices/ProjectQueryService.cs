using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Queries;
using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;
using eb4341u202318323.API.Maintenance.Domain.Repositories;
using eb4341u202318323.API.Maintenance.Domain.Services;

namespace eb4341u202318323.API.Maintenance.Application.Internal.QueryServices;

public class ProjectQueryService(IProjectRepository projectRepository) : IProjectQueryService
{
    public async Task<Project?> Handle(GetProjectByIdQuery query)
    {
        return await projectRepository.FindByIdAsync(query.ProjectId);
    }
    
    public async Task<Project?> Handle(GetProjectByCodeProjectQuery query)
    {
        var codeProject = CodeProject.Create(query.CodeProject);
        var projects = await projectRepository.FindByCodeProject(codeProject);
        return projects.FirstOrDefault();
    }
}