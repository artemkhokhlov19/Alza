using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Alza.Core.Data.Extensions;

public static class DbContextExtensions
{
    public static void RollbackChanges(this DbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        foreach (EntityEntry item in context.ChangeTracker.Entries())
        {
            if (item.State != 0 && item.State != EntityState.Unchanged)
            {
                item.State = EntityState.Unchanged;
            }
        }
    }
}
