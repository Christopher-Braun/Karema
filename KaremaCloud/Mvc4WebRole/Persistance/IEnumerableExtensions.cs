using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Mvc4WebRole
{
    public static class IEnumerableExtensions
    {
        public static void Remove<T>(this List<T> list, Func<T, bool> predicate)
        {
            list.Where(predicate).ToList().ForEach((t) => list.Remove(t));
        }

        public static IEnumerable<Int32> GetMatchingIndeces<T>(this IEnumerable<T> enumeration, Func<T, bool> predicate)
        {
            var list = enumeration.ToList();
            for ( int i = 0; i < list.Count; i++ )
            {
                if ( predicate(list[i]) )
                {
                    yield return i;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach ( var value in enumeration )
            {
                action(value);
            }
        }

        public static IEnumerable<T> IgnoreNulls<T>(this IEnumerable<T> enumeration)
        {
            if ( ReferenceEquals(enumeration, null) )
            {
                return Enumerable.Empty<T>();
            }

            return enumeration.Where(t => !ReferenceEquals(t, null));
        }

        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> enumeration)
        {
            return (enumeration == null || !enumeration.Any());
        }


        public static Boolean IsNotEmpty<T>(this IEnumerable<T> enumeration)
        {
            return !enumeration.IsNullOrEmpty();
        }

        public static IEnumerable<T> MakeEnumerable<T>(this T entity) where T:class
        {
            if ( entity != null )
            {
                yield return entity;
            }
        }

        public static Int32 IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return obj.IndexOf(value, 0);
        }

        public static Int32 IndexOf<T>(this IEnumerable<T> obj2, T value, Int32 startIndex)
        {
            var obj = obj2.ToList();
            if ( startIndex >= obj.Count() )
            {
                return -1;
            }

            if ( startIndex < 0 )
            {
                throw new IndexOutOfRangeException("Der StartIndex darf nicht kleiner als 0 sein.");
            }

            for ( Int32 i = startIndex; i < obj.Count(); ++i )
            {
                if ( value.Equals(obj.ElementAt(i)) )
                {
                    return i;
                }
            }

            return -1;
        }

        //public static Int32 NearestIndexOf(this IEnumerable<Double> doubleArray, Double value)
        //{
        //    Double arrayValue = doubleArray.Select(d => new { Value = d, Distance = Math.Abs(value - d) }).OrderBy(m => m.Distance).First().Value;
        //    return doubleArray.IndexOf(arrayValue);

        //}
    }
}