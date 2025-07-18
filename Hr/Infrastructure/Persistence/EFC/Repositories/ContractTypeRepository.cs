using eb4341u202318323.API.Hr.Domain.Model.Entities;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using eb4341u202318323.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eb4341u202318323.API.Hr.Infrastructure.Persistence.EFC.Repositories;

public class ContractTypeRepository(AppDbContext context) : BaseRepository<ContractType>(context), IContractTypeRepository
{
    public async Task AddRangeAsync(IEnumerable<ContractType> contractTypes)
    {
        await Context.Set<ContractType>().AddRangeAsync(contractTypes);
    }

    public async Task<bool> AnyAsync()
    {
        return await Context.Set<ContractType>().AnyAsync();
    }
}
