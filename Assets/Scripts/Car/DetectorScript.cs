using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public class DetectorScript : MonoBehaviour
{
    [SerializeField] private float _Length;
    [SerializeField] private LayerMask _CollisionMask;
    [SerializeField] private Collider _SelfCollider;

    [SerializeField] private UnityEvent<Collider[]> OnCollisionDetected;
    [SerializeField] private UnityEvent OnCollisionCleared;

    private bool _hasCollisions;

    private void FixedUpdate()
    {
        DetectCollision();
    }

    /// <summary>
    /// Runs the collision detection for the raycast
    /// If it detects a collision, it fires the OnCollisionDetected event with the array of hit colliders
    /// </summary>
    private void DetectCollision()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        List<RaycastHit> hits = Physics.RaycastAll(ray, _Length, _CollisionMask)?.ToList();
        hits?.Remove(hits.FirstOrDefault(x => x.collider == _SelfCollider)); // Avoid self-collision
        if (hits != null && hits.Count > 0)
        {
            if (!_hasCollisions)
            {
                Collider[] colliders = hits.Select(x => x.collider).ToArray();
                OnCollisionDetected?.Invoke(colliders);
                _hasCollisions = true;
            }
        }
        else if (_hasCollisions)
        {
            OnCollisionCleared?.Invoke();
            _hasCollisions = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * _Length));
    }
}
