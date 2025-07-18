using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;

namespace eb4341u202318323.API.Maintenance.Interfaces.ACL;

public interface IMaintenanceContextFacade
{
    Task<Project?> FetchProjectByCodeProject(Guid codeProject);
    Task<bool> RequestPersonnelBudgetReduction(Guid codeProject, double amountToReduce);
}