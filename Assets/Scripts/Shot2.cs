using UnityEngine;

public class Shot2 : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] protected float bulletSpeed = 50f;
    [SerializeField] protected float timeLife = 5f;

    [Header("Explosion")]
    [SerializeField] protected GameObject _explosion;
    [SerializeField] protected float _explosionRadius = 5f;
    [SerializeField] protected float _explosionForce = 10f;
    [SerializeField] protected float _explosionUpWrand = 3f;


    public void Fly(Vector3 flyDirection, float torQue)
    {
        Rigidbody rbBullet = GetComponent<Rigidbody>();
        rbBullet.AddTorque(Random.insideUnitSphere.normalized * torQue, ForceMode.Impulse);
        rbBullet.AddForce (flyDirection * bulletSpeed, ForceMode.Impulse);
        Destroy(gameObject, timeLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(_explosion, collision.contacts[0].point,collision.transform.localRotation);
        var explosionTarget = Physics.OverlapSphere(collision.contacts[0].point, _explosionRadius);
        foreach(var rbTarget in explosionTarget)
        {
            if (rbTarget.CompareTag("Target"))
            {
                rbTarget.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce,collision.contacts[0].point,_explosionRadius,_explosionUpWrand,ForceMode.Impulse);
            }
        }
        Destroy(explosion, 3f);
        Destroy(gameObject);
    }
}
