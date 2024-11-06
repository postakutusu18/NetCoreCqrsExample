using Core.Persistance.Repositories;
using Domains;

public interface IProductDalAsync : IAsyncRepository<Product,Guid> { }
