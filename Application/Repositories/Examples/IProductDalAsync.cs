using Core.Persistance.Repositories;
using Domains.Examples;

public interface IProductDalAsync : IAsyncRepository<Product,Guid> { }
