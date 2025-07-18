using eb4341u202318323.API.Hr.Domain.Model.ValueObjects;
using eb4341u202318323.API.Shared.Domain.Model.Events;

namespace eb4341u202318323.API.Hr.Domain.Model.Events;

/// <summary>
/// Evento que se emite cuando el presupuesto de personal de un proyecto cambia.
/// </summary>
public record ChangePersonnelBudgetEvent : IEvent 
{
    /// <summary>
    /// Obtiene el CodeProject del proyecto afectado.
    /// </summary>
    public CodeProject CodeProject { get; init; }

    /// <summary>
    /// Obtiene el nuevo valor del presupuesto de personal en USD.
    /// </summary>  
    public double NewPersonnelBudgetUsd { get; init; }

    /// <summary>
    /// Constructor para el evento ChangePersonnelBudgetEvent.
    /// </summary>
    /// <param name="codeProject">El CodeProject del proyecto.</param>
    /// <param name="newPersonnelBudgetUsd">El nuevo presupuesto de personal en USD.</param>
    public ChangePersonnelBudgetEvent(CodeProject codeProject, double newPersonnelBudgetUsd)
    {
        CodeProject = codeProject;
        NewPersonnelBudgetUsd = newPersonnelBudgetUsd;
    }
}
