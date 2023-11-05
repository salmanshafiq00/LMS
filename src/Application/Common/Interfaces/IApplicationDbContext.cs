using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<CommonLookup> CommonLookups { get; }
    DbSet<CommonLookupDetail> CommonLookupDetails { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
