using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Interfaces.REST.Resources;

namespace eb4341u202318323.API.Hr.Interfaces.REST.Transform;

public static class EmployeeResourceFromEntityAssembler
{
    public static EmployeeResource toResourceFromEntity(Employee entity)
    {
        return new EmployeeResource
        (
            entity.Id,
            entity.Name,
            entity.MonthlySalary,
            entity.ContractDurationMonths,
            entity.CodeProject.Code,
            entity.StartDate,
            entity.ContractType.Name);
    }
    
}