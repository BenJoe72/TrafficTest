using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionScript : MonoBehaviour
{
    private List<Collider> Occupants;

    private void Start()
    {
        Occupants = new List<Collider>();
    }

    /// <summary>
    /// Adds a new occupant to the intersection
    /// </summary>
    /// <param name="occupant">The car entering the intersection</param>
    public void OccupyIntersection(Collider occupant)
    {
        if (!Occupants.Contains(occupant))
            Occupants.Add(occupant);
    }

    /// <summary>
    /// Removes and occupant from the intersection
    /// </summary>
    /// <param name="occupant">The car leaving the intersection</param>
    public void FreeIntersection(Collider occupant)
    {
        if (Occupants.Contains(occupant))
            Occupants.Remove(occupant);
    }

    /// <summary>
    /// Checks if the intersection is free or not
    /// </summary>
    /// <returns>Whether the intersection is free or not</returns>
    public bool CheckFree()
    {
        Debug.Log(Occupants.Count);
        return Occupants.Count <= 0;
    }
}
