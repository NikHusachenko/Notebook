using Microsoft.EntityFrameworkCore;

namespace Notebook.Services.Extensions;

public static class PagerHelper
{
    public static async Task<List<T>> Paging<T>(this IQueryable<T> query, int pageVolume, int page = 1) where T : class
    {
        int skip = page <= 1 ? 0 : (page - 1) * pageVolume;
        return await query.Skip(skip).Take(pageVolume).ToListAsync();
    }
}