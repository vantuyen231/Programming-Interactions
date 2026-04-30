using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] protected bool isRagdoll = false;
    [SerializeField] protected Animator animator;
    [SerializeField] protected List<Rigidbody> rigidbodies = new List<Rigidbody>();
    [SerializeField] protected List<Collider> colliders = new List<Collider>();

    private void Awake()
    {
        TryGetComponent<Animator>(out animator);

        if(animator == null) return;

        GetComponentsInChildren(rigidbodies);
        GetComponentsInChildren(colliders);

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = true;
            colliders[i].isTrigger = true;
        }
    }
    private void Update()
    {
        if (isRagdoll)
        {
            EnableRagdoll();
        }
    }

    private void EnableRagdoll()
    {
        isRagdoll = !isRagdoll;
        for(int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].linearVelocity = Vector3.zero;
            colliders[i].isTrigger = false;
        }
        animator.enabled = false;
    }
}
