using eb4341u202318323.API.Hr.Domain.Model.Commands;
using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Hr.Domain.Model.Entities;

namespace eb4341u202318323.API.Hr.Domain.Model.Aggregates;

public partial class Employee 
{
    public int Id { get; protected set; }
    public string Name { get; set; } = string.Empty;
    public double MonthlySalary { get; set; }
    public int ContractDurationMonths { get; set; }
    public CodeProject CodeProject { get; private set; } = null!;
    public DateTime StartDate { get; set; }
    public ContractType ContractType { get; set; } = null!;
    
    public Employee() { }

    public Employee(string name, double monthlySalary, int contractDurationMonths, Guid code, DateTime startDate,
        ContractType contractType)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        if (monthlySalary <= 0) throw new ArgumentException("MonthlySalary must be positive.");
        if (contractDurationMonths <= 0) throw new ArgumentException("ContractDurationMonths must be positive.");
        if (code == Guid.Empty) throw new ArgumentException("CodeProject cannot be empty.");
        if (contractType is null) throw new ArgumentNullException(nameof(contractType), "ContractType is required.");

        Name = name;
        MonthlySalary = monthlySalary;
        ContractDurationMonths = contractDurationMonths;
        CodeProject = CodeProject.Create(code);
        StartDate = startDate;
        ContractType = contractType;
    }
    
    public Employee(CreateEmployeeCommand command, ContractType contractType) : this(command.Name,command.MonthlySalary, command.ContractDurationMonths, command.CodeProject, command.StartDate, contractType)
    {
            
    }
    
}