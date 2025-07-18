namespace eb4341u202318323.API.Hr.Domain.Model.Commands;

public record CreateEmployeeCommand(string Name, double MonthlySalary, int ContractDurationMonths, Guid CodeProject, DateTime StartDate, int ContractTypeId);