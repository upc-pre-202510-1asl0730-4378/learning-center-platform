namespace eb4341u202318323.API.Hr.Domain.Model.Entities;

public class ContractType
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Hours { get; set; }

    public ContractType()
    {
        
    }
    
    public ContractType(string name, int hours)
    {
        Name = name;
        Hours = hours;
    }

    public static ContractType FromValue(int value)
    {
        return value switch
        {
            1 => new ContractType("FULL TIME", 40),
            2 => new ContractType("PART TIME", 20),
            3 => new ContractType("FIXED TERM", 30),
            4 => new ContractType("HOURLY", 15), 
            _ => throw new ArgumentException("Unknown contract type value: " + value)
        };
    }
}

