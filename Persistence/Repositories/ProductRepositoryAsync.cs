using Core.Persistance.Repositories;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ProductRepositoryAsync : EfAsyncRepositoryBase<Product,Guid>, IProductDalAsync
{
    public ProductRepositoryAsync(DbContext context) : base(context)
    {
    }
}
