using eb4341u202318323.API.Hr.Domain.Model.Commands;
using eb4341u202318323.API.Hr.Domain.Model.Entities;
using eb4341u202318323.API.Hr.Domain.Repositories;
using eb4341u202318323.API.Hr.Domain.Services;
using eb4341u202318323.API.Shared.Domain.Repositories;

namespace eb4341u202318323.API.Hr.Application.Internal.CommandServices;

public class ContractTypeCommandService(IContractTypeRepository contractTypeRepository, IUnitOfWork unitOfWork, ILogger<ContractTypeCommandService> logger) : IContractTypeCommandService
{
    public async Task<bool> Handle(SeedContractTypesCommand command)
    {
        try
        {
            if (await contractTypeRepository.AnyAsync())
            {
                logger.LogInformation("ContractTypes already exist in the database. Skipping seeding.");
                return true;
            }

            var contractTypesToSeed = new List<ContractType>
            {
                new ContractType("FULL TIME", 40) { Id = 1 },
                new ContractType("PART TIME", 20) { Id = 2 },
                new ContractType("FIXED TERM", 30) { Id = 3 },
                new ContractType("HOURLY", 15) { Id = 4 }
            };
            await contractTypeRepository.AddRangeAsync(contractTypesToSeed);
            
            await unitOfWork.CompleteAsync();

            logger.LogInformation("ContractTypeCommandService: SeedContractTypesCommand handled successfully and data saved via repository.");
            return true;
        }
        catch (System.Exception ex)
        {
            logger.LogError(ex, "ContractTypeCommandService: Error handling SeedContractTypesCommand.");
            return false;
        }
    }
}