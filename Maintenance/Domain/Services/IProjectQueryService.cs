using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Queries;

namespace eb4341u202318323.API.Maintenance.Domain.Services;

public interface IProjectQueryService
{
    Task<Project?> Handle(GetProjectByIdQuery query);
    Task<Project?> Handle(GetProjectByCodeProjectQuery query);
}