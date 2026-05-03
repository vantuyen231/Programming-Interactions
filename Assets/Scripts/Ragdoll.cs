using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] protected bool isRagdoll = false;

    [SerializeField] public CapsuleCollider capsuleCollider;

    [Header("Animator")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isSitting = false;
    [SerializeField] protected bool isDancing = false;

    [Header("List")]
    [SerializeField] public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    [SerializeField] public List<Collider> colliders = new List<Collider>();

    [Header("Audio")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] protected int pitchMax = 3;
    [SerializeField] protected int pitchMin = 1;
    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out capsuleCollider);
        TryGetComponent(out audioSource);


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
        if(isRagdoll) { return; }
        isRagdoll = !isRagdoll;
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.Play();
        for (int i = 0; i < rigidbodies.Count; i++)
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
