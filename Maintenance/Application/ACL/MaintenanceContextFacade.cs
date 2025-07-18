using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Domain.Model.Queries;
using eb4341u202318323.API.Maintenance.Domain.Services;
using eb4341u202318323.API.Maintenance.Interfaces.ACL;

namespace eb4341u202318323.API.Maintenance.Application.ACL;

public class MaintenanceContextFacade(IProjectCommandService projectCommandService, IProjectQueryService projectQueryService) : IMaintenanceContextFacade
{
    public async Task<Project?> FetchProjectByCodeProject(Guid codeProject)
    {
        var getProjectByCodeProjectQuery = new GetProjectByCodeProjectQuery(codeProject);
        var project = await projectQueryService.Handle(getProjectByCodeProjectQuery);
        return project;
    }
    
    public async Task<bool> RequestPersonnelBudgetReduction(Guid codeProject, double amountToReduce)
    {
        // 1. Crear un comando para el Bounded Context de Mantenimiento
        // Este comando ya está definido en Maintenance.Domain.Model.Commands
        var updateCommand = new UpdateProjectPersonnelBudgetCommand(codeProject, amountToReduce);

        // 2. Enviar el comando al servicio de comandos del Bounded Context de Mantenimiento
        // Asumiendo que IProjectCommandService tiene un método para manejar esta actualización
        var updatedProject = await projectCommandService.Handle(updateCommand);

        // Retornar true si el proyecto fue actualizado con éxito (no es nulo)
        return updatedProject != null;
    }
}