using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Interfaces.REST.Resources;

namespace eb4341u202318323.API.Maintenance.Interfaces.REST.Transform;

public static class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project entity)
    {
        return new ProjectResource(
            entity.Id,
            entity.CodeProject.Code.ToString(),
            entity.ProjectName,
            entity.Type.ToString(),
            entity.MaterialsBudgetUsd,
            entity.PersonnelBudgetUsd,
            entity.DurationMonths);
    }
}