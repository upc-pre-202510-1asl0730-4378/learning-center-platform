using eb4341u202318323.API.Maintenance.Domain.Model.Commands;
using eb4341u202318323.API.Maintenance.Domain.Model.ValueObjects;

namespace eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;

public partial class Project
{
    public int Id { get; protected set; }
    public CodeProject CodeProject { get; private set; } = null!;
    public string ProjectName { get; set; } = string.Empty;
    public double MaterialsBudgetUsd { get; set; }
    public double PersonnelBudgetUsd { get; set; }
    public int DurationMonths { get; set; }
    public EConstructionType Type { get; protected set; }

    public Project() { }

    public Project(Guid code, string projectName, EConstructionType type, double materialsBudgetUsd, double personnelBudgetUsd, int durationMonths)
    {
        if (code == Guid.Empty) throw new ArgumentException("CodeProject no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(projectName)) throw new ArgumentException("ProjectName es obligatorio.");
        if (!Enum.IsDefined(typeof(EConstructionType), type)) throw new ArgumentException("ConstructionType inválido.");
        if (materialsBudgetUsd <= 0) throw new ArgumentException("MaterialsBudgetUsd debe ser positivo.");
        if (personnelBudgetUsd <= 0) throw new ArgumentException("PersonnelBudgetUsd debe ser positivo.");
        if (durationMonths <= 0) throw new ArgumentException("DurationMonths debe ser positivo.");

        CodeProject = CodeProject.Create(code);
        ProjectName = projectName;
        Type = type;
        MaterialsBudgetUsd = materialsBudgetUsd;
        PersonnelBudgetUsd = personnelBudgetUsd;
        DurationMonths = durationMonths;
    }
    
    public Project(CreateProjectCommand command)
        : this(command.CodeProject, command.ProjectName, command.ConstructionType, command.MaterialsBudgetUsd, command.PersonnelBudgetUsd, command.DurationMonths) { }
    /// <summary>
    /// Reduces the personnel budget of the project.
    /// </summary>
    /// <param name="amount">The amount to reduce the budget by.</param>
    /// <exception cref="ArgumentException">Thrown if the amount is not positive or if it exceeds the current budget.</exception>
    public void ReducePersonnelBudget(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount to reduce must be positive.", nameof(amount));
        }
        if (PersonnelBudgetUsd < amount)
        {
            throw new ArgumentException($"Cannot reduce budget by {amount} USD. Current budget is {PersonnelBudgetUsd} USD.", nameof(amount));
        }
        PersonnelBudgetUsd -= amount;
    }
}