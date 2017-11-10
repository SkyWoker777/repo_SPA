using LibrarySPA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySPA.Core.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<TParent> Map<TParent, TChild, TKey>
            (
            this Dapper.SqlMapper.GridReader reader,
            Func<TParent, TKey> firstKey,
            Func<TChild, TKey> secondKey,
            Action<TParent, TChild> addChildren)
        {
            var first = reader.Read<TParent>().ToList();
            var childMap = reader
                .Read<TChild>()
                .GroupBy(s => secondKey(s))
                .ToDictionary(g => g.Key, g => g.FirstOrDefault<TChild>());

            foreach (var item in first)
            {
                TChild child;
                if (childMap.TryGetValue(firstKey(item), out child))
                {
                    addChildren(item, child);
                }
            }

            return first;
        }

        public static TParent MapForSingle<TParent, TChild, TKey>
            (
            this Dapper.SqlMapper.GridReader reader,
            Func<TParent, TKey> firstKey,
            Func<TChild, TKey> secondKey,
            Action<TParent, TChild> addChildren)
        {
            var first = reader.Read<TParent>().FirstOrDefault();
            var childMap = reader
                .Read<TChild>()
                .GroupBy(s => secondKey(s))
                .ToDictionary(g => g.Key, g => g.FirstOrDefault<TChild>());

                TChild child;
                if (childMap.TryGetValue(firstKey(first), out child))
                {
                    addChildren(first, child);
                }

            return first;
        }

    }
}
