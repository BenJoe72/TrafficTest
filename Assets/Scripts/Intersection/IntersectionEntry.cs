using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class IntersectionEntry : MonoBehaviour
{
    [SerializeField] private UnityEvent<IntersectionEntry> _OnEntryEntered;
    [SerializeField] private UnityEvent<IntersectionEntry> _OnEntryEmpty;

    public bool IsFree { get { return _occupants.Count <= 0; } }

    private List<Collider> _occupants;

    private void Start()
    {
        _occupants = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_occupants.Contains(other))
        {
            if (IsFree)
                _OnEntryEntered?.Invoke(this);

            _occupants.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_occupants.Contains(other))
            _occupants.Remove(other);

        if (IsFree)
            _OnEntryEmpty?.Invoke(this);
    }
}
