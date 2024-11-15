namespace Domains.Examples;

public class ExampleEntity : Entity<Guid>
{
    public string? Name { get; set; }
    public int Code { get; set; }
}
