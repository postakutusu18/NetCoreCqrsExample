using Core.Persistance.Repositories;
using Domains.Examples;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Examples;

public class ProductRepositoryAsync : EfAsyncRepositoryBase<Product, Guid>, IProductDalAsync
{
    public ProductRepositoryAsync(DbContext context) : base(context)
    {
    }
}
