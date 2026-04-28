using UnityEngine;

public class Shot : MonoBehaviour
{
    //[SerializeField] protected float force = 10f;
    //private Rigidbody rb;

    //private void Update()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.AddForce(transform.forward * force, ForceMode.Impulse);

    //    Destroy(gameObject, 5f);
    //}

    [SerializeField] protected float timeLife = 4f;
    [SerializeField] protected GameObject explosionprefabs;
    [SerializeField] protected float _explosionForce = 10f;
    [SerializeField] protected float _explosionRadius = 5f;
    [SerializeField] protected float _explosionUpWard = 1f;


    private void Update()
    {
        //Destroy(gameObject,timeLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(explosionprefabs,transform.position,transform.rotation);
        var rbExplostion = Physics.OverlapSphere(collision.contacts[0].point, _explosionRadius);
        foreach(var rb in rbExplostion)
        {
            if (rb.CompareTag("Target"))
            {
                rb.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, collision.contacts[0].point, _explosionRadius, _explosionUpWard, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
