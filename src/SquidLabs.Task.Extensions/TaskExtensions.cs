namespace SquidLabs.Task.Extensions;

public static class TaskExtensions
{
    public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> inputTask, Func<TIn, Task<TOut>> mapping,
        CancellationToken cancellationToken = default)
    {
        var input = await inputTask.ConfigureAwait(false);
        return await mapping(input).ConfigureAwait(false);
    }
}