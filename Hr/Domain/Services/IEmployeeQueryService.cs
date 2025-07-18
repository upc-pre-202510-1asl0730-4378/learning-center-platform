using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using eb4341u202318323.API.Hr.Domain.Model.Queries;

namespace eb4341u202318323.API.Hr.Domain.Services;

public interface IEmployeeQueryService
{
    Task<Employee?> Handle(GetEmployeeByIdQuery query);
    Task<Employee?> Handle(GetEmployeeByCodeProjectQuery query);
}