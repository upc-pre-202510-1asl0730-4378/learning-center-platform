using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Interfaces.ACL;

namespace eb4341u202318323.API.Hr.Application.Internal.OutboundServices;

public class ExternalMaintenanceService(IMaintenanceContextFacade maintenanceContextFacade)
{
    /// <summary>
    /// Constructor del servicio.
    /// </summary>
    /// <param name="codeProject">La fachada ACL para el contexto de Mantenimiento.</param>
    
    /// <inheritdoc />
    public async Task<Project?> FetchProjectByCodeProject(Guid codeProject)
    {
        return await maintenanceContextFacade.FetchProjectByCodeProject(codeProject);
    }

    /// <inheritdoc />
    public async Task<bool> RequestPersonnelBudgetReduction(Guid codeProject, double amountToReduce)
    {
        return await maintenanceContextFacade.RequestPersonnelBudgetReduction(codeProject, amountToReduce);
    }
}
