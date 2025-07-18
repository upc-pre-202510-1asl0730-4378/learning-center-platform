using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Interfaces.REST.Resources;

namespace eb4341u202318323.API.Maintenance.Interfaces.REST.Transform;

public static class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource)
    {
        return new CreateProjectCommand(
            resource.CodeProject,
            resource.ProjectName,
            resource.ConstructionType,
            resource.MaterialsBudgetUsd,
            resource.PersonnelBudgetUsd,
            resource.DurationMonths);
    }
}