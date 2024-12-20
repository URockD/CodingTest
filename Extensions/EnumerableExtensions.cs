using System;
using System.Collections.Generic;

namespace CodingTest.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Batch size must be greater than 0.", nameof(size));
            }

            var batch = new List<T>(size);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>(size);
                }
            }

            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }

}
