using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Ragdollize : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Collider _sphere;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private bool _makeRagdoll = false;

    public UnityEvent RagdollEvent;
    
    private List<Collider> _colliders = new List<Collider>();
    private List<Rigidbody> _rbs = new List<Rigidbody>();

    private void Start()
    {
        _colliders = GetComponentsInChildren<Collider>().ToList();

        _colliders.Remove(_mainCollider);
        _colliders.Remove(_sphere);

        _rbs = GetComponentsInChildren<Rigidbody>().ToList();

        _rbs.Remove(_rb);

        _animator.enabled = true;
        _mainCollider.enabled = true;

        foreach (var item in _colliders)
        {
            if (item != _sphere || item != _mainCollider)
            {
                item.isTrigger = true;
            }
        }

        foreach (var item in _rbs)
        {
            if (item != _rb)
            {
                item.isKinematic = true; 
            }
        }
    }

    private void Update()
    {
        if (_makeRagdoll) StartCoroutine(nameof(Ragdoll));
    }

    private IEnumerator Ragdoll()
    {
        RagdollEvent.Invoke();

        yield return new WaitForSeconds(.1f);

        foreach (var item in _colliders)
        {
            if (item != _sphere || item != _mainCollider)
            {
                item.isTrigger = false; 
            }
        }

        foreach (var item in _rbs)
        {
            if (item != _rb)
            {
                item.isKinematic = false; 
            }
        }

        Destroy(gameObject, 10);
    }
}
