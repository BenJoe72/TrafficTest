using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPointScript : MonoBehaviour
{
    /// <summary>
    /// Array of the connected intersections, set in editor
    /// </summary>
    [SerializeField] protected IntersectionPointScript[] _Desinations;
    [SerializeField] protected IntersectionEntry[] _ClearEntries;

    /// <summary>
    /// Returns the intersection's transform's position
    /// </summary>
    public Vector3 Position { get { return transform.position; } }

    /// <summary>
    /// Gets a new random intersection from the preset connections list
    /// </summary>
    /// <returns>A randomly selected intersection</returns>
    public IntersectionPointScript GetNextIntersection()
    {
        return _Desinations.GetRandomElement();
    }

    /// <summary>
    /// Checks if the intersection is free
    /// </summary>
    /// <returns>Whether the itersection is free or not</returns>
    public virtual bool CheckFree()
    {
        bool result = true;

        foreach (var entry in _ClearEntries)
        {
            result &= entry.IsFree;
        }

        return result;
    }

    protected void OnDrawGizmosSelected()
    {
        if (_Desinations == null)
            return;

        foreach (var dest in _Desinations)
        {
            if (dest == null)
                continue;

            Gizmos.DrawLine(transform.position, dest.transform.position);
        }
    }
}
