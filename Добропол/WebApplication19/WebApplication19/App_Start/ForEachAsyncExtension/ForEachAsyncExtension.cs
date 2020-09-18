using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication19.ForEachAsyncExtension
{
    public static class ForEachAsyncExtension
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body, CancellationToken cancellationToken = default)
        {
            if (dop > source.Count())
            {
                dop = source.Count();
            }
            return Task.WhenAll(from partition in Partitioner.Create(source).GetPartitions(dop)
                                select Task.Run(async delegate
                                {
                                    using (partition)
                                    {
                                        while (partition.MoveNext())
                                        {
                                            await body(partition.Current).ConfigureAwait(false);
                                        }
                                    }
                                }, cancellationToken));
        }
    }
}