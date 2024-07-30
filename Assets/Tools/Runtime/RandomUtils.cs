using System.Collections.Generic;
using System.Linq;

namespace Tools.Runtime
{
    public static class RandomUtils
    {
        public static void Shuffle<T>(this IList<T> array, System.Random seed = null)
        {
            seed ??= new System.Random();
            var n = array.Count;
            while (n > 1)
            {
                var k = seed.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
        
        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount, System.Random seed = null)
        {
            var listCopy = new List<T>(list);
            listCopy.Shuffle(seed);
            return listCopy.Take(elementsCount);
        }
        
        public static T GetRandomElement<T>(this IList<T> array)
        {
            var id = UnityEngine.Random.Range(0, array.Count);
            return array[id];
        }

        public static T GetRandomElement<T>(params T[] arr)
        {
            return arr.GetRandomElement();
        }

        public static T GetProbabilityElement<T>(this IDictionary<T, float> itemProbabilityMap)
        {
            var probabilitySum = itemProbabilityMap.Sum(pair => pair.Value);
            var randomValue = UnityEngine.Random.value;
            var ratio = 1f / probabilitySum;
            var tempProbability = 0f;
            foreach (var pair in itemProbabilityMap)
            {
                tempProbability += pair.Value;
                if (randomValue / ratio <= tempProbability)
                {
                    return pair.Key;
                }
            }

            return default;
        }
        
        public static List<T> GetProbabilityElements<T>(this IDictionary<T, float> itemProbabilityMap)
        {
            List<T> result = new List<T>();
            foreach (var pair in itemProbabilityMap)
            {
                if (UnityEngine.Random.value <= pair.Value)
                {
                    result.Add(pair.Key);
                }
            }

            return result;
        }
    }
}