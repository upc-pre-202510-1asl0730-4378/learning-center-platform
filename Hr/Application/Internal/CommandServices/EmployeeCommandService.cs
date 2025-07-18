using eb4341u202318323.API.Hr.Application.Internal.OutboundServices;
using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Commands;
using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Hr.Domain.Services;
using IUnitOfWork = eb4341u202318323.API.Shared.Domain.Repositories.IUnitOfWork;

namespace eb4341u202318323.API.Hr.Application.Internal.CommandServices;

public class EmployeeCommandService(IEmployeeRepository employeeRepository,ExternalMaintenanceService externalMaintenanceService, IContractTypeRepository contractTypeRepository, IUnitOfWork unitOfWork, ILogger<EmployeeCommandService> logger) : IEmployeeCommandService
{
    public async Task<Employee?> Handle(CreateEmployeeCommand command)
    {
        if (command.StartDate.Date <= DateTime.UtcNow.Date) // Compare dates only
        {
            throw new ArgumentException("Start date must be a future date.");
        }

        var project = await externalMaintenanceService.FetchProjectByCodeProject(command.CodeProject);
        if (project == null)
        {
            throw new ArgumentException($"Invalid CodeProject: {command.CodeProject} not registered in Projects.");
        }

        if (command.ContractDurationMonths >= project.DurationMonths)
        {
            throw new ArgumentException($"Contract duration ({command.ContractDurationMonths} months) must be less than project duration ({project.DurationMonths} months).");
        }
        
        var contractType = await contractTypeRepository.FindByIdAsync(command.ContractTypeId); 
        if (contractType == null)
        {
            throw new ArgumentException($"Invalid ContractType ID: {command.ContractTypeId}.");
        }

        var employeeCodeProject = CodeProject.Create(command.CodeProject);
        if (await employeeRepository.ExistsByNameAndCodeProjectAndContractType(command.Name, employeeCodeProject, command.ContractTypeId))
        {
            throw new InvalidOperationException($"An employee with name '{command.Name}', CodeProject '{command.CodeProject}', and ContractType '{contractType.Name}' already exists.");
        }

        double totalSalary = command.MonthlySalary * command.ContractDurationMonths;

        var budgetReduced = await externalMaintenanceService.RequestPersonnelBudgetReduction(
            command.CodeProject,
            totalSalary);

        if (!budgetReduced)
        {
            throw new InvalidOperationException("Failed to reduce project personnel budget. Insufficient funds or project not found.");
        }

        var employee = new Employee(
            command.Name,
            command.MonthlySalary,
            command.ContractDurationMonths,
            command.CodeProject,
            command.StartDate,
            contractType
        );

        await employeeRepository.AddAsync(employee);
        await unitOfWork.CompleteAsync(); 

        logger.LogInformation($"Employee '{employee.Name}' created with ID {employee.Id} and project budget reduced.");

        return employee;
    }
}