using Core.Persistance.Repositories;

namespace Domains.Examples;

public class Product : Entity<Guid>
{
    // public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    //public Product()
    //{

    //}
    //public Product(Guid id,string name,decimal price)
    //{
    //    Id = id;
    //    Name = name;
    //    Price = price;

    //}
}
