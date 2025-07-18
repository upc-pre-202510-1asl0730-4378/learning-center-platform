using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Queries;
using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Hr.Domain.Services;

namespace eb4341u202318323.API.Hr.Application.Internal.QueryServices;

public class EmployeeQueryService(IEmployeeRepository employeeRepository) : IEmployeeQueryService
{
    public async Task<Employee?> Handle(GetEmployeeByIdQuery query)
    {
        return await employeeRepository.FindByIdAsync(query.EmployeeId);
    }
    public async Task<Employee?> Handle(GetEmployeeByCodeProjectQuery query)
    {
        var codeProject = CodeProject.Create(query.CodeProject);
        var employees = await employeeRepository.FindByCodeProject(codeProject);
        return employees.FirstOrDefault();
    }
}