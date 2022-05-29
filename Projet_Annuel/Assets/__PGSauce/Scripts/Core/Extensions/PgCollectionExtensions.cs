using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGSauce.Core.PGDebugging;
using PGSauce.Internal.Attributes;
using PGSauce.PgRandom;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// This class provides useful extension methods for collections, mostly IEnumerable and Lists.
    /// </summary>
    [Version(0,0,1)]
    public static class PgCollectionExtensions
    {
        /// <summary>
        /// Returns all elements of the source which are of FilterType.
        /// </summary>
        public static IEnumerable<TFilter> FilterByType<T, TFilter>(this IEnumerable<T> source)
            where T : class
            where TFilter : class, T
        {
            return source.Where(item => item is TFilter).Cast<TFilter>();
        }
        
        /// <summary>
        /// Removes all the elements in the list that does not satisfy the predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The list to remove elements from.</param>
        /// <param name="predicate">The predicate used to filter elements. 
        /// All elements that don't satisfy the predicate will be matched.</param>
        public static void RemoveAllBut<T>(this List<T> source, Predicate<T> predicate)
        {
            bool Inverse(T item) => !predicate(item);

            source.RemoveAll(Inverse);
        }
        
        /// <summary>
        /// Returns whether this collection is empty.
        /// </summary>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }
        
        /// <summary>
        /// Add all elements of other to the given collection.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> other)
        {
            if (other == null)
            {
                return;
            }

            foreach (var obj in other)
            {
                collection.Add(obj);
            }
        }
        
        /// <summary>
        /// Performs an action on each element of the collection
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
	        foreach (var item in collection)
	        {
		        action(item);
	        }
        }
        
        /// <summary>
        /// Performs an action on each element of the sequence and allows for a return type
        /// </summary>
        public static List<TReturn> ForEach<T, TReturn>(this IEnumerable<IEnumerable<T>> sequence, Func<T, Vector2Int, TReturn> function)
        {
	        var results = new List<TReturn>();
	        if (sequence == null)
	        {
		        return results;
	        }

	        var column = 0;
	        using var columnEnum = sequence.GetEnumerator();
	        while (columnEnum.MoveNext())
	        {
		        var row = 0;
		        if (columnEnum.Current != null)
		        {
			        using var rowEnum = columnEnum.Current.GetEnumerator();
			        while (rowEnum.MoveNext())
			        {
				        results.Add(function.Invoke(rowEnum.Current, new Vector2Int(column, row)));
				        ++row;
			        }
		        }
		        ++column;
	        }
	        return results;
        }

        /// <summary>
        /// Returns a pretty string representation of the given list. The resulting string looks something like
        /// <c>[a, b, c]</c>.
        /// </summary>
        public static string ListToString<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return "null";
            }

            if (!source.Any())
            {
                return "[]";
            }

            if (source.Count() == 1)
            {
                return "[" + source.First() + "]";
            }

            var s = "";

            s += source.ButFirst().Aggregate(s, (res, x) => res + ", " + x.ListToString());
            s = "[" + source.First().ListToString() + s + "]";

            return s;
        }

        /// <summary>
        /// Prints in a human readable way the dictionary [key : value, key : value, ...]
        /// </summary>
        /// <param name="dict">The dictionary to print</param>
        /// <typeparam name="TKey">Type of the keys</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <returns>Formatted representation of the dictionary</returns>
        public static string PrettyPrint<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
	        if (dict.Count <=  0)
	        {
		        return "[]";
	        }
	        
	        var sb = new StringBuilder();
	        sb.Append("[");

	        foreach (var pair in dict.ButFirst())
	        {
		        sb.Append($"{pair.Key} : {pair.Value}; ");
	        }

	        var p = dict.First();
	        
	        sb.Append($"{p.Key} : {p.Value}");
	        
	        sb.Append("]");

	        return sb.ToString();
        }
        
        /// <summary>
        /// Returns an enumerable of all elements of the given list	but the first,
        /// keeping them in order.
        /// </summary>
        public static IEnumerable<T> ButFirst<T>(this IEnumerable<T> source)
        {
            return source.Skip(1);
        }
        
        /// <summary>
        /// Returns an enumerable of all elements in the given 
        /// list but the last, keeping them in order.
        /// </summary>
        public static IEnumerable<T> ButLast<T>(this IEnumerable<T> source)
        {
            var lastX = default(T);
            var first = true;

            foreach (var x in source)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    yield return lastX;
                }

                lastX = x;
            }
        }
        
        /// <summary>
        /// Returns a enumerable with elements in order, but the first element is moved to the end.
        /// </summary>
        public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> source)
        {
            var enumeratedList = source as IList<T> ?? source.ToList();
            return enumeratedList.ButFirst().Concat(enumeratedList.Take(1));
        }

        /// <summary>
        /// Returns a enumerable with elements in order, but the last element is moved to the front.
        /// </summary>
        public static IEnumerable<T> RotateRight<T>(this IEnumerable<T> source)
        {
            var enumeratedList = source as IList<T> ?? source.ToList();
            yield return enumeratedList.Last();

            foreach (var item in enumeratedList.ButLast())
            {
                yield return item;
            }
        }
        
        /// <summary>
		/// Returns a random element from a source.
		/// </summary>
		/// <typeparam name="T">The type of items generated from the source.</typeparam>
		/// <param name="source">The list.</param>
		/// <returns>A item ramdonly selected from the source.</returns>
		public static T RandomItem<T>(this IEnumerable<T> source)
		{
			return RandomItem(source, new ConcretePgRandom());
		}

		public static T RandomItem<T>(this IEnumerable<T> source, IRandom random)
		{
			return source.SampleRandom(1, random).First();
		}

		/// <summary>
		/// Returns a random sample from a source.
		/// </summary>
		/// <typeparam name="T">The type of elements of the source.</typeparam>
		/// <param name="source">The source from which to sample.</param>
		/// <param name="sampleCount">The number of samples to return.</param>
		/// <returns>Generates a ransom subset from a given source.</returns>
		public static IEnumerable<T> SampleRandom<T>(this IEnumerable<T> source, int sampleCount)
		{
			return SampleRandom(source, sampleCount, new ConcretePgRandom());
		}

		/// <summary>
		/// Returns a random sample from a source.
		/// </summary>
		/// <typeparam name="T">The type of elements of the source.</typeparam>
		/// <param name="source">The source from which to sample.</param>
		/// <param name="sampleCount">The number of samples to return.</param>
		/// <param name="random">The random generator to use.</param>
		/// <returns>Generates a ransom subset from a given source.</returns>
		public static IEnumerable<T> SampleRandom<T>(
			this IEnumerable<T> source, 
			int sampleCount, 
			IRandom random)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (sampleCount < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(sampleCount));
			}

			/* Reservoir sampling. */
			var samples = new List<T>();

			//Must be 1, otherwise we have to use Range(0, i + 1)
			var i = 1;

			foreach (var item in source)
			{
				if (i <= sampleCount)
				{
					samples.Add(item);
				}
				else
				{
					// Randomly replace elements in the reservoir with a decreasing probability.
					var r = random.Next(i);

					if (r < sampleCount)
					{
						samples[r] = item;
					}
				}

				i++;
			}

			return samples;
		}

		/// <summary>
		/// Shuffles a list.
		/// </summary>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <param name="list">The list to shuffle.</param>
		public static void Shuffle<T>(this IList<T> list)
		{
			list.Shuffle(new ConcretePgRandom());
		}

		/// <summary>
		/// Shuffles a list.
		/// </summary>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <param name="list">The list to shuffle.</param>
		/// <param name="random">The random generator to use.</param>
		public static void Shuffle<T>(this IList<T> list, IRandom random)  
		{  
		    var n = list.Count;  
		    
			while (n > 1) 
			{  
		        n--;
				var k = random.Next(0, n + 1);
				list.Swap(k, n);
			}  
		}
		
		/// <summary>
		/// Returns an random int that is in the list range 
		/// </summary>
		public static int GetRandomIndex<T>(this IEnumerable<T> list)
		{
			return Random.Range(0, list.Count());
		}
		
		/// <summary>
		/// Returns true if the index is not within the list bounds
		/// </summary>
		public static bool OutOfRange<T>(this IEnumerable<T> list, int index)
		{
			return !(index >= 0 && index < list.Count());
		}
		
		/// <summary>
		/// Swap two elements in the list
		/// </summary>
		public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
		{
			if (list == null)
			{
				PGDebug.Message("Can't swap indexes, the list is null").LogError();
				return;
			}
			if (list.OutOfRange(firstIndex) || list.OutOfRange(secondIndex))
			{
				PGDebug.Message($"Can't swap indexes, the list is out of range : {Mathf.Max(firstIndex, secondIndex)} > {list.Count}").LogError();
				return;
			}
			(list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);
		}
		
		/// <summary>
		/// Returns the first half of elements from a source.
		/// </summary>
		public static IEnumerable<T> TakeHalf<T>(this IEnumerable<T> source)
		{
			var enumerable = source as T[] ?? source.ToArray();
			var count = enumerable.Length;

			return enumerable.Take(count/2);
		}

		/// <summary>
		/// Returns the last n elements from a source.
		/// </summary>
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n)
		{
			var takeLast = source as T[] ?? source.ToArray();
			var count = takeLast.Count();

			return count <= n ? takeLast : takeLast.Skip(count - n);
		}
		
		/// <summary>
		/// If the sequence is null, creates an empty list
		/// </summary>
		public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> sequence)
		{
			return sequence ?? new List<T>();
		}
		
		/// <summary>
		/// Get the element with the max property
		/// </summary>
		public static TList GetMaxByProperty<TList>(this IEnumerable<TList> list, Func<TList, float> evaluator)
		{
			return list.Aggregate(((a1, a2) => evaluator(a1) > evaluator(a2) ? a1 : a2));
		}
		
		/// <summary>
		/// Get the element with the min property
		/// </summary>
		public static TList GetMinByProperty<TList>(this IEnumerable<TList> list, Func<TList, float> evaluator)
		{
			return list.Aggregate(((a1, a2) => evaluator(a1) < evaluator(a2) ? a1 : a2));
		}

		/// <summary>
		/// Adds the element in the list if it is not in here
		/// </summary>
		public static void AddUnique<T>(this List<T> list, T element)
		{
			if(list.Contains(element)){return;}
			list.Add(element);
		}
        
        private static string ListToString(this object obj)
        {
	        if (obj is string objAsString) return objAsString;

            var objAsList = obj as IEnumerable;

            return objAsList == null ? obj.ToString() : objAsList.Cast<object>().ListToString();
        }
    }
}