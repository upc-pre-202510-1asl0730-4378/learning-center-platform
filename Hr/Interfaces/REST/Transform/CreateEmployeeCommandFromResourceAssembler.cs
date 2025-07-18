using eb4341u202318323.API.Hr.Domain.Model.Commands;
using eb4341u202318323.API.Hr.Interfaces.REST.Resources;

namespace eb4341u202318323.API.Hr.Interfaces.REST.Transform;

public static class CreateEmployeeCommandFromResourceAssembler
{
    public static CreateEmployeeCommand toCommandFromResource(CreateEmployeeResource resource)
    {
        return new CreateEmployeeCommand(
            resource.Name,
            resource.MonthlySalary,
            resource.ContractDurationMonths,
            resource.CodeProject,
            resource.StartDate,
            resource.ContractTypeId
        );
    }
}