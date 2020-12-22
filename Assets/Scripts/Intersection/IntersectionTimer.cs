using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class IntersectionTimer : MonoBehaviour
{
    [SerializeField] private float _SwitchTime;
    [SerializeField] private float _SwitchDelay;
    [SerializeField] private IntersectionGroup[] _IntersectionGroups;

    private int _currentIntersection;
    private float _nextSwitchTime;
    private bool _filled;
    private List<IntersectionGroup> _filledGroups;

    private IntersectionGroup _curretnIntersectionGroup { get { return _IntersectionGroups[_currentIntersection]; } }

    private void Start()
    {
        _filledGroups = new List<IntersectionGroup>();
        Switch();
    }
    
    private void Update()
    {
        if (_filledGroups.Count > 0 && Time.time > _nextSwitchTime)
            Switch();
    }

    /// <summary>
    /// Tries switching to the entry
    /// </summary>
    /// <param name="entry">The entry to switch to</param>
    public void TrySwitch(IntersectionEntry entry)
    {
        IntersectionGroup group = _IntersectionGroups.FirstOrDefault(x => x._Entry == entry);
        if (group != null && !_filledGroups.Contains(group))
        {
            _filledGroups.Add(group);

            if (_filledGroups.IndexOf(group) == 0)
                Switch();
        }
    }

    /// <summary>
    /// Removes the empty entry from the groups list
    /// </summary>
    /// <param name="entry">The entry to remove</param>
    public void ClearEntry(IntersectionEntry entry)
    {
        IntersectionGroup group = _IntersectionGroups.FirstOrDefault(x => x._Entry == entry);
        if (group != null && _filledGroups.Contains(group))
        {
            Switch();
        }
    }

    /// <summary>
    /// Switches the intersection
    /// </summary>
    public void Switch()
    {
        int nextIntersection = 0;

        if (_filledGroups.Contains(_curretnIntersectionGroup))
            _filledGroups.Remove(_curretnIntersectionGroup);

        if (_filledGroups.Count > 0)
        {
            nextIntersection = Array.IndexOf(_IntersectionGroups, _filledGroups.First());

            // If there are still cars waiting we add the waiting group to the end of the line
            if (!_curretnIntersectionGroup._Entry.IsFree)
                _filledGroups.Add(_curretnIntersectionGroup);
        }

        SwitchTo(nextIntersection);
    }

    /// <summary>
    /// Switched to the set index
    /// </summary>
    /// <param name="index">The index to switch to</param>
    private void SwitchTo(int index)
    {
        DisableCurrentIntersections();

        _currentIntersection = index;

        Invoke("EnableCurrentIntersections", _SwitchDelay);

        _nextSwitchTime = Time.time + _SwitchTime;
    }

    /// <summary>
    /// Rotates the current intersection index
    /// </summary>
    private int IncrementCurrentIntersection()
    {
        int result = _currentIntersection + 1;
        if (result >= _IntersectionGroups.Length || result < 0)
            result = 0;
        return result;
    }

    /// <summary>
    /// Disables the current interesections
    /// </summary>
    private void DisableCurrentIntersections()
    {
        SetCurrentIntersections(false);
    }

    /// <summary>
    /// Enables the current interesctions
    /// </summary>
    private void EnableCurrentIntersections()
    {
        SetCurrentIntersections(true);
    }

    /// <summary>
    /// Sets the current intersections to a set value
    /// </summary>
    /// <param name="value">Whether the intersections should be enabled or not</param>
    private void SetCurrentIntersections(bool value)
    {
        foreach (var intersection in _curretnIntersectionGroup._Intersections)
            intersection.SetFree(value);
    }

    /// <summary>
    /// Helper class for grouping intersections into intersection groups
    /// </summary>
    [System.Serializable]
    public class IntersectionGroup
    {
        public TimedIntersectionPointScript[] _Intersections;
        public IntersectionEntry _Entry;
    }
}
