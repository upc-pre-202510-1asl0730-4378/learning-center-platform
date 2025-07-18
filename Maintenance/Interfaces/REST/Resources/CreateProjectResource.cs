using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;

namespace eb4341u202318323.API.Maintenance.Interfaces.REST.Resources;

public record CreateProjectResource(Guid CodeProject, string ProjectName, EConstructionType ConstructionType, double MaterialsBudgetUsd, double PersonnelBudgetUsd, int DurationMonths);