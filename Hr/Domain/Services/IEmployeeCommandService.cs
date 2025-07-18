using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Commands;

namespace eb4341u202318323.API.Hr.Domain.Services;

public interface IEmployeeCommandService
{
    Task<Employee?> Handle(CreateEmployeeCommand command);
}