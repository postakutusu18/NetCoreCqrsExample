using Core.Persistance.Repositories;
using Domains;
using Persistence.Context;

namespace Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product,Guid, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
}
