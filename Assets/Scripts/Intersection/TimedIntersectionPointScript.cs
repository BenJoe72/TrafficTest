using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedIntersectionPointScript : IntersectionPointScript
{
    private bool _isFree;

    /// <summary>
    /// Sets whether the interesction is free or not
    /// </summary>
    /// <param name="free">Whether the interesction is free or not</param>
    public void SetFree(bool free)
    {
        _isFree = free;
    }

    /// <summary>
    /// Checks if the intersection is free
    /// </summary>
    /// <returns>Whether the itersection is free or not</returns>
    public override bool CheckFree()
    {
        return _isFree;
    }
}
