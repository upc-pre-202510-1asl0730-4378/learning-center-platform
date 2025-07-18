using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;
using eb4341u202318323.API.Maintenance.Domain.Repositories;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb4341u202318323.API.Maintenance.Infrastructure.Persistence.EFC.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> FindByCodeProject(CodeProject codeProject)
    {
        return await Context.Set<Project>()
            .Where(project => project.CodeProject.Code == codeProject.Code)
            .ToListAsync();
    }

    public async Task<bool> ExistsByCodeProjectAndProjectName(CodeProject codeProject, string projectName)
    {
        return await Context.Set<Project>()
            .AnyAsync(project => project.CodeProject.Code == codeProject.Code && project.ProjectName == projectName);
    }
}