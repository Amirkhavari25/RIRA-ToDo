using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Infrastructure.Extentions
{
    public static class QueryableExtensions
    {
        public static Task<List<T>> ToListAsyncSafe<T>(this IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return queryable.ToListAsync(cancellationToken);
        }
    }
}
