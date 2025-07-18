using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Entities;
using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Shared.Domain.Repositories;

namespace eb4341u202318323.API.Hr.Domain.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<IEnumerable<Employee>> FindByCodeProject(CodeProject codeProject);
    Task<bool> ExistsByNameAndCodeProjectAndContractType(string name, CodeProject codeProject, int contractTypeId);
}