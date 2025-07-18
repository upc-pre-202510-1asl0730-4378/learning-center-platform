using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Entities;
using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb4341u202318323.API.Hr.Infrastructure.Persistence.EFC.Repositories;

public class EmployeeRepository(AppDbContext context) : BaseRepository<Employee>(context), IEmployeeRepository
{   
    public async Task<IEnumerable<Employee>> FindByCodeProject(CodeProject codeProject)
    {
        return await Context.Set<Employee>()
            .Where(employee => employee.CodeProject.Code == codeProject.Code)
            .ToListAsync();
    }
    public async Task<bool> ExistsByNameAndCodeProjectAndContractType(string name, CodeProject codeProject, int contractTypeId)
    {
        return await Context.Set<Employee>()
            .AnyAsync(employee => employee.Name == name && 
                                  employee.CodeProject.Code == codeProject.Code && 
                                  employee.ContractType.Id == contractTypeId);
    }
}