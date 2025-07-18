namespace eb4341u202318323.API.Maintenance.Interfaces.REST.Resources;

public record ProjectResource(int Id, string CodeProject, string ProjectName, string ConstructionType, double MaterialsBudgetUsd, double PersonnelBudgetUsd, int DurationMonths);