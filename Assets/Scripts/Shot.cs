using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] protected float timeLife = 5f;
    [SerializeField] protected GameObject _explosion;
    [SerializeField] protected float explosionDamge = 10f;
    [SerializeField] protected float explosionRadius = 5f;
    [SerializeField] protected float upWrand = 1f;

    private void Update()
    {
        //Destroy(gameObject, timeLife);
    }

    private void OnCollisionEnter(Collision collision)
    {

        var explosion = Instantiate(_explosion, collision.contacts[0].point,transform.rotation);
        var _radiusExplosion = Physics.OverlapSphere(collision.contacts[0].point, explosionRadius);
        foreach(var rb in _radiusExplosion)
        {
            var ragdoll = rb.GetComponent<Ragdoll>();
            if (rb.CompareTag("Target"))
            {
                ragdoll.EnableRagdoll();
                foreach(var rbRagdoll in ragdoll.rigidbodies)
                {
                    rbRagdoll.AddExplosionForce(explosionDamge, collision.contacts[0].point, explosionRadius, upWrand, ForceMode.Impulse);
                }
                rb.GetComponent<Rigidbody>().AddExplosionForce(explosionDamge,collision.contacts[0].point,explosionRadius,upWrand,ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
