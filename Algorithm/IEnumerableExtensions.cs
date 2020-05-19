using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWorkDO.Algorithm
{
      public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(
            this IEnumerable<T> source,
            int chunksize)
        {
            var pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize);
                pos += chunksize;
            }
        }    
    }
}
