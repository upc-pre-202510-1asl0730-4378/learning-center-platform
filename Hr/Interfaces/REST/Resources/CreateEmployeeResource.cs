namespace eb4341u202318323.API.Hr.Interfaces.REST.Resources;

public record CreateEmployeeResource(string Name, double MonthlySalary, int ContractDurationMonths, Guid CodeProject, DateTime StartDate, int ContractTypeId);
