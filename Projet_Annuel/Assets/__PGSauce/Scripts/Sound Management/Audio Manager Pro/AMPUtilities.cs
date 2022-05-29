using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>Various extension functions used by AudioManagerPro.</summary>
namespace PGSauce.AudioManagement.External.Utilities
{
    /// <summary>Collection of string formatting utilities.</summary>
    public static class StringFormatting
    {
        /// <summary>Formats a duration in seconds into a time string.</summary>
        /// <param name="Duration">The duration in seconds.</param>
        /// <returns>Formatted time string.</returns>
        public static string FormatTime(float Duration)
        {
            //Converts the time into hours, minutes and seconds
            int Minutes = ((int)Duration) / 60;
            int Hours = Minutes / 60;
            Minutes = Minutes % 60;
            float Seconds = Duration - 60 * Minutes;

            //Uses 1 d.p. for seconds if less than a minute long
            if (Minutes > 0 || Hours > 0) { Seconds = Mathf.Round(Seconds); }
            else { Seconds = NumberManipulation.DecimalPlaces(Seconds, 1); }

            //Constructs the string
            string TimeString;
            TimeString = Seconds.ToString() + "s";
            if (Minutes > 0 || Hours > 0)
            {
                TimeString = Minutes.ToString() + "m " + TimeString;
                if (Hours > 0) { TimeString = Hours.ToString() + "h " + TimeString; }
            }

            //Returns formatted string
            return TimeString;
        }
    }

    /// <summary>Collection of number manipulation utilities.</summary>
    public static class NumberManipulation
    {
        /// <summary>Rounds the number to a desired number of decimal places.</summary>
        /// <param name="Number">The number to be rounded.</param>
        /// <param name="dp">The number of decimal places to round it to.</param>
        /// <returns>The rounded number.</returns>
        public static float DecimalPlaces(float Number, int dp) { return Mathf.Round(Number * Mathf.Pow(10, dp)) / Mathf.Pow(10, dp); }
    }

    /// <summary>Extends the List class.</summary>
    public static class ListExtension
    {
        /// <summary>Returns a random index valid within this list.</summary>
        /// <returns>The random index.</returns>
        public static int RandomIndex<T>(this List<T> Arr) { return (int)Random.Range(0f, Arr.Count - 0.001f); }

        /// <summary> Returns a random element from the list.</summary>
        /// <returns> A random object from the list.</returns>
        public static T RandomElement<T>(this List<T> Arr)
        {
            if (Arr.Count == 0) { throw new System.IndexOutOfRangeException("The list was empty."); }
            return Arr[Arr.RandomIndex()];
        }

        /// <summary>Removes and returns an element from the list.</summary>
        /// <param name="Index">The index of the desired element.</param>
        /// <returns>The element at the specified index.</returns>
        public static T Pop<T>(this List<T> Arr, int Index)
        {
            if (Index < 0 || Index >= Arr.Count || Arr.Count == 0) { throw new System.ArgumentOutOfRangeException("Index", Index, "The index specified was out of bounds of the list"); }
            T Item = Arr[Index];
            Arr.RemoveAt(Index);
            return Item;
        }

        /// <summary>Pops a random element from the list.</summary>
        /// <returns>The removed element.</returns>
        public static T PopRandom<T>(this List<T> Arr)
        {
            if (Arr.Count == 0) { throw new System.IndexOutOfRangeException("The list was empty."); }
            return Arr.Pop(Arr.RandomIndex());
        }

        /// <summary>Randomizes the order of the list.</summary>
        public static void Randomize<T>(this List<T> Arr)
        {
            List<T> Temp = new List<T>(Arr);
            Arr.Clear();
            while (Temp.Count > 0) { Arr.Add(Temp.PopRandom()); }
        }
    }

    /// <summary>Extension class for all IEnumerables</summary>
    public static class IEnumerableExtension
    {
        /// <summary>Returns a copy of the IEnumerable with all duplicates removed.</summary>
        /// <param name="IDSelector">The lambda expression to define the unique ID of the element.</param>
        /// <returns>The filtered IEnumerable.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TID>(this IEnumerable<TSource> Arr, System.Func<TSource, TID> IDSelector)
        {
            HashSet<TID> KnownIDs = new HashSet<TID>();
            return Arr.Where(x => KnownIDs.Add(IDSelector(x)));
        }
    }
}
