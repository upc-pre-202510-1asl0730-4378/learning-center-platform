using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Commands;

namespace eb4341u202318323.API.Maintenance.Domain.Services;

public interface IProjectCommandService
{
    Task<Project> Handle(CreateProjectCommand command);
    Task<Project?> Handle(UpdateProjectPersonnelBudgetCommand command);
}