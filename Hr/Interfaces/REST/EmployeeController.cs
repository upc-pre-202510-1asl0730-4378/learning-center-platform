using System.Net.Mime;
using eb4341u202318323.API.Hr.Domain.Services;
using eb4341u202318323.API.Hr.Interfaces.REST.Resources;
using eb4341u202318323.API.Hr.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace eb4341u202318323.API.Hr.Interfaces.REST;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Get all employees")]
public class EmployeeController(IEmployeeCommandService employeeCommandService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create Employee", Description = "Create a new employee in the system.",
        OperationId = "CreateEmployee")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(EmployeeResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Request")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeResource resource)
    {
        var createEmployeeCommand = CreateEmployeeCommandFromResourceAssembler.toCommandFromResource(resource);
        
        var employee = await employeeCommandService.Handle(createEmployeeCommand);
        
        if (employee is null)
        {
            return BadRequest("Employee creation failed.");
        }
        
        var createdResource = EmployeeResourceFromEntityAssembler.toResourceFromEntity(employee);
        
        return StatusCode(StatusCodes.Status200OK, createdResource);
    }
}