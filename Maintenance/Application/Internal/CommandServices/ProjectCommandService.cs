using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Domain.Model.Queries;
using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;
using eb4341u202318323.API.Maintenance.Domain.Repositories;
using eb4341u202318323.API.Maintenance.Domain.Services;
using eb4341u202318323.API.Shared.Domain.Repositories;

namespace eb4341u202318323.API.Maintenance.Application.Internal.CommandServices;

public class ProjectCommandService(IProjectQueryService projectQueryService, IProjectRepository projectRepository, IUnitOfWork unitOfWork) : IProjectCommandService
{
    public async Task<Project> Handle(CreateProjectCommand command)
    {
        var codeProject = CodeProject.Create(command.CodeProject);
        var existingProject = await projectQueryService.Handle(new GetProjectByCodeProjectQuery(codeProject.Code));

        if (existingProject != null)
        {
            throw new InvalidOperationException($"A project with code {command.CodeProject} already exists.");
        }

        var project = new Project(command);

        await projectRepository.AddAsync(project);
        await unitOfWork.CompleteAsync();

        return project;
    }
    public async Task<Project?> Handle(UpdateProjectPersonnelBudgetCommand command)
    {
        // 1. Fetch the project by its CodeProject
        var getProjectByCodeProjectQuery = new GetProjectByCodeProjectQuery(command.CodeProject);
        var project = await projectQueryService.Handle(getProjectByCodeProjectQuery);

        if (project == null)
        {
            // Project not found, cannot update budget
            return null;
        }

        // 2. Apply the budget reduction (assuming Project aggregate has a method for this)
        // You'll need to add a method like 'ReducePersonnelBudget' to your Project aggregate
        project.ReducePersonnelBudget(command.AmountToReduce); 

        // 3. Update the project in the repository
        projectRepository.Update(project);
        await unitOfWork.CompleteAsync();

        return project;
    }
}