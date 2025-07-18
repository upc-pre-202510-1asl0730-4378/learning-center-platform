using eb4341u202318323.API.Hr.Domain.Model.Commands;

namespace eb4341u202318323.API.Hr.Domain.Services;

/// <summary>
/// Interfaz para el servicio de comandos de ContractType.
/// Define las operaciones de comando disponibles para los tipos de contrato.
/// </summary>
public interface IContractTypeCommandService
{
    /// <summary>
    /// Maneja el comando para sembrar los tipos de contrato iniciales.
    /// </summary>
    /// <param name="command">El comando SeedContractTypesCommand.</param>
    /// <returns>Una tarea que representa la operación asíncrona, devolviendo true si la siembra fue exitosa.</returns>
    Task<bool> Handle(SeedContractTypesCommand command);
}
