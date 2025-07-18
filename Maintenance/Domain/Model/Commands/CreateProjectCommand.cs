using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;

namespace eb4341u202318323.API.Maintenance.Domain.Model.Commands;

public record CreateProjectCommand(Guid CodeProject, string ProjectName, EConstructionType ConstructionType, double MaterialsBudgetUsd, double PersonnelBudgetUsd, int DurationMonths);