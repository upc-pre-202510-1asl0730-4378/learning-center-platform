namespace eb4341u202318323.API.Hr.Domain.Model.ValueObjects;

public record CodeProject
{
    public Guid Code { get; }
    private CodeProject(Guid code)
    {
        Code = code;
    }
    
    public static CodeProject Create(Guid code)
    {
        if (code == Guid.Empty) 
            throw new ArgumentException("Code cannot be empty.", nameof(Code)); 
        return new CodeProject(code);
    }
}