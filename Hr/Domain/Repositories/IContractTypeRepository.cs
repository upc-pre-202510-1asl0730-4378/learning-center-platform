using eb4341u202318323.API.Hr.Domain.Model.Entities;
using eb4341u202318323.API.Shared.Domain.Repositories;

namespace eb4341u202318323.API.Hr.Domain.Repositories;

public interface IContractTypeRepository : IBaseRepository<ContractType>
{
    /// <summary>
    /// Agrega una colección de tipos de contrato de forma asíncrona.
    /// </summary>
    /// <param name="contractTypes">La colección de ContractType a agregar.</param>
    Task AddRangeAsync(IEnumerable<ContractType> contractTypes);

    /// <summary>
    /// Verifica de forma asíncrona si existen tipos de contrato en la base de datos.
    /// </summary>
    /// <returns>True si existen tipos de contrato; de lo contrario, false.</returns>
    Task<bool> AnyAsync();
}
