namespace eb4341u202318323.API.Hr.Interfaces.REST.Resources;

public record EmployeeResource(int Id, string Name, double MonthlySalary, int ContractDurationMonths, Guid CodeProject, DateTime StartDate, string ContractType);