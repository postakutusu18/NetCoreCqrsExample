using Core.Persistance.Repositories;
using Domains.Examples;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Examples;

public class ExampleEntityRepositoryAsync : EfAsyncRepositoryBase<ExampleEntity, Guid>, IExampleEntityDalAsync
{
    public ExampleEntityRepositoryAsync(DbContext context) : base(context)
    {
    }
}

