using System.Net.Mime;
using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Domain.Model.Queries;
using eb4341u202318323.API.Maintenance.Domain.Services;
using eb4341u202318323.API.Maintenance.Interfaces.REST.Resources;
using eb4341u202318323.API.Maintenance.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eb4341u202318323.API.Maintenance.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Endpoints")]
public class ProjectsController(IProjectCommandService projectCommandService,
    IProjectQueryService projectQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create Project", Description = "Creates a new project in the system.",
        OperationId = "CreateProject")]
    [SwaggerResponse(StatusCodes.Status201Created, "Project Created", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Request")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectResource resource)
    {
        var createProjectCommand = CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var project = await projectCommandService.Handle(createProjectCommand);
        if (project is null)
        {
            return BadRequest("Project creation failed.");
        }
        
        var createdResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        
        // Se ha eliminado el método GET, por lo que no se puede usar CreatedAtAction.
        // En su lugar, se devuelve un StatusCode 201 Created con el recurso creado en el cuerpo.
        return StatusCode(StatusCodes.Status201Created, createdResource);
    }
}