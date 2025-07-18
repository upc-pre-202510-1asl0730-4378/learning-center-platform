using eb4341u202318323.API.Hr.Domain.Model.Events;
using eb4341u202318323.API.Shared.Application.Internal.EventHandler;

namespace eb4341u202318323.API.Hr.Application.Internal.EventHandlers;

/// <summary>
/// Handles the ChangePersonnelBudgetEvent by logging the budget reduction.
/// </summary>
public class ChangePersonnelBudgetEventHandler(ILogger<ChangePersonnelBudgetEventHandler> logger) : IEventHandler<ChangePersonnelBudgetEvent>
{
    /// <summary>
    /// Handles the ChangePersonnelBudgetEvent.
    /// </summary>
    /// <param name="domainEvent">The ChangePersonnelBudgetEvent to handle.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    public Task Handle(ChangePersonnelBudgetEvent domainEvent, CancellationToken cancellationToken)
    {
        // Implement the business rule: "mostrar un mensaje de info vía Logger2 en consola
        // indicando “Project: The value of personnel Budget del Project X was reduced to Y USD”
        // donde X es el codeProject e Y es el valor de personnelBudgetUsd."
        logger.LogInformation(
            "Project: The value of personnel Budget del Project {CodeProject} was reduced to {NewPersonnelBudgetUsd} USD",
            domainEvent.CodeProject.Code.ToString(), // Access the Guid from CodeProject and convert to string
            domainEvent.NewPersonnelBudgetUsd);

        return Task.CompletedTask;
    }
}
