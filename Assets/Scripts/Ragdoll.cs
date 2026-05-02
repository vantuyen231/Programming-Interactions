using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] protected bool isRagdoll = false;
    [SerializeField] protected Animator animator;

    [SerializeField] public CapsuleCollider capsuleCollider;

    [SerializeField] protected bool isSitting = false;
    [SerializeField] protected bool isDancing = false;

    [Header("List")]
    [SerializeField] public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> colliders = new List<Collider>();

    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out capsuleCollider);

        if(animator == null) return;

        GetComponentsInChildren(rigidbodies);
        GetComponentsInChildren(colliders);

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = true;
            colliders[i].isTrigger = true;
        }
        capsuleCollider.isTrigger = false;
        UpdateAnimation();
    }
    //private void Update()
    //{
    //    UpdateAnimation();
    //}

    public void EnableRagdoll()
    {
        isRagdoll = !isRagdoll;
        for(int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].linearVelocity = Vector3.zero;
            colliders[i].isTrigger = false;
        }
        capsuleCollider.isTrigger = true;
        animator.enabled = false;
    }

    private void UpdateAnimation()
    {
        if(animator == null || !animator.enabled) { return; }
        animator.SetBool("isSit",isSitting);
        animator.SetBool("isDance", isDancing);
    }
}
