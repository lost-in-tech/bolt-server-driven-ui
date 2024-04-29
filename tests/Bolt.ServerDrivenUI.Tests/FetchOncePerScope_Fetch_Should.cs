using Bolt.Endeavor;
using Shouldly;

namespace Bolt.ServerDrivenUI.Tests;

public class FetchOncePerScope_Fetch_Should
{
    [Fact]
    public async Task Fetch_only_once_per_scope()
    {
        var sut = new FetchOncePerScope<int>();

        var tasks = new List<Task<MaySucceed<int>>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(sut.Fetch("testing", i, CancellationToken.None, static async (_, ct) =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10), ct);
                return ExecuteCounter.Execute();
            }));
        }

        await Task.WhenAll(tasks);
        
        ExecuteCounter.Count.ShouldBe(1);
    }

    private static class ExecuteCounter
    {
        public static int Count { get; private set; }

        public static MaySucceed<int> Execute()
        {
            Count++;

            return Count;
        }
    }
}