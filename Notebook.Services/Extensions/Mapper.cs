using Notebook.Services.ResultService;

namespace Notebook.Services.Extensions;

public static class Mapper
{
    public static async Task<IEnumerable<TOut>> Map<TIn, TOut>(this Task<IEnumerable<TIn>> asyncResult, Func<TIn, TOut> mapper)
    {
        IEnumerable<TIn> list = await asyncResult;
        return list.Select(mapper);
    }

    public static async Task<TOut> Map<TIn, TOut>(this Task<TIn> asyncResult, Func<TIn, TOut> mapper)
    {
        TIn result = await asyncResult;
        return mapper(result);
    }

    public static TOut Map<TIn, TOut>(this TIn result, Func<TIn, TOut> mapper) => mapper(result);
}