using System.Collections.Generic;
using System.Linq;

namespace Tools.Runtime
{
    public static class RandomUtils
    {
        public static void Shuffle<T>(this IList<T> array, int? seed = null)
        {
            var rng = new System.Random(seed ?? UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            var n = array.Count;
            while (n > 1)
            {
                var k = rng.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }
}