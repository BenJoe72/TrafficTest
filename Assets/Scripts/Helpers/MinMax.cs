using UnityEngine;
using System.Collections;

public abstract class MinMax<T>
{
    public T Min;
    public T Max;

    /// <summary>
    /// Checks if the value porvided is inside the min-max range
    /// </summary>
    /// <param name="value">The value you want to check the min-max against</param>
    /// <param name="allowExtremes">Inclue equaling to the extremes in the check</param>
    /// <returns>Whether the value is inside the range</returns>
    public abstract bool InRange(T value, bool allowExtremes = true);

    /// <summary>
    /// Returns a random value between the minimum and the maximum
    /// </summary>
    /// <param name="allowExtremes">Inclue the extremes in the random generation</param>
    /// <returns></returns>
    public abstract T GetRandomValue();
}

[System.Serializable]
public class MinMaxFloat : MinMax<float>
{
    public override bool InRange(float value, bool allowExtremes = true)
    {
        if (allowExtremes)
            return value >= Min && value <= Max;
        else
            return value > Min && value < Max;        
    }

    public override float GetRandomValue()
    {
        return Random.Range(Min, Max);
    }
}
