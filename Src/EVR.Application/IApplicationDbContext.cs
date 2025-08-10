using Microsoft.EntityFrameworkCore;

namespace EVR.Application
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
