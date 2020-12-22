using UnityEngine;
using System.Collections;

public static class RandomHelper
{
    /// <summary>
    /// Simulates a coin flip
    /// </summary>
    /// <returns>Returns true or false depending on the outcome of the coin flip</returns>
    public static bool CoinFlip()
    {
        return Random.Range(0f, 1f) > .5f;
    }

    /// <summary>
    /// Returns a random element from the array
    /// </summary>
    /// <typeparam name="T">Type of array elements</typeparam>
    /// <param name="array">The input array that the random element is chosen from</param>
    /// <returns>A randomly selected element from the input array</returns>
    public static T GetRandomElement<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
