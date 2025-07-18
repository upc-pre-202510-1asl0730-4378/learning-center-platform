namespace eb4341u202318323.API.Maintenance.Domain.Model.Commands;

public record UpdateProjectPersonnelBudgetCommand(Guid CodeProject, double AmountToReduce);