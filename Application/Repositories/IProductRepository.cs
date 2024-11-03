using Core.Persistance.Repositories;
using Domains;

public interface IProductRepository : IAsyncRepository<Product,Guid>, IRepository<Product, Guid> { }
