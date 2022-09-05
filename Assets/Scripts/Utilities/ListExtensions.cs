using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class ListExtensions
    {
        private static readonly Random Rnd = new Random();
        
        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this List<T> list)
        {
            var listCopy = list.ToList();
            listCopy.Shuffle();
            return listCopy[0];
        }
    }
}