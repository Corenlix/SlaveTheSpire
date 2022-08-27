using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class ListShuffler
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
    }
}