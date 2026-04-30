using TMPro;
using UnityEngine;

public class CannonController_02 : MonoBehaviour
{
    private SimpleControls _simpleControls2;
    [SerializeField] protected bool isClamp = true;
    [Header("Barrel")]
    [SerializeField] private Transform barrel2;
    [SerializeField] protected Vector2 _aimDirection2 = Vector2.zero;
    [SerializeField] protected float aimSpeed2 = 10f;
    [SerializeField] protected float aimBarrelMax = 20f;
    [SerializeField] protected float aimBarrelMin = -20f;

    [Header("TurnCannon")]
    [SerializeField] private Vector2 _turnCannon;
    [SerializeField] protected float turnSpeed2 = 10f;
    [SerializeField] protected float turnBarrelMax = 30f;
    [SerializeField] protected float turnBarrelMin = -30f;

    [Header("RollWheel")]
    [SerializeField] protected Vector2 wheelRotation;
    [SerializeField] protected Vector2 wheelRotation2;
    [SerializeField] protected float wheelSpeed = 10f;
    [SerializeField] protected Transform wheel1;
    [SerializeField] protected Transform wheel2;
    [SerializeField] protected Transform wheel3;
    [SerializeField] protected Transform wheel4;

    [Header("Fire")]
    [SerializeField] protected Transform firePoint2;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float torQueForce = 3f;
    [SerializeField] protected float canFire = 2f;
    [SerializeField] protected float cdFire = 0f;

    [Header("Many Bullet")]
    [SerializeField] protected TMP_Text countBullet;
    [SerializeField] protected int countBulletMax = 8;
    [SerializeField] protected GameObject noAmmo;


    private void Awake()
    {
        _simpleControls2 = new SimpleControls();
        _turnCannon = transform.localEulerAngles;
        turnBarrelMax += _turnCannon.y;
        turnBarrelMin += _turnCannon.y;
        UpdateCountBullet();
    }

    private void OnEnable()
    {
        _simpleControls2.Enable();
        _simpleControls2.gameplay.fire.performed += ctx => Fire();
        _simpleControls2.gameplay.menu.performed += ctx => { MenuManager.Instance.Show(); };
        _simpleControls2.gameplay.reload.performed += ctx => ReloadBullet();
    }

    private void OnDisable()
    {
        _simpleControls2.Disable();
    }

    private void Update()
    {
        Vector2 move2 = _simpleControls2.gameplay.move.ReadValue<Vector2>();
        this.Aim2(move2.y);
        this.Turn(move2.x);
        cdFire += Time.deltaTime;
    }

    private void Aim2(float aimDirection2)
    {
        float scaleAim = _aimDirection2.x + aimDirection2 * aimSpeed2 * Time.deltaTime;
        _aimDirection2.x = Mathf.Clamp(scaleAim, aimBarrelMin, aimBarrelMax);
        barrel2.localEulerAngles = _aimDirection2;
    }

    private void Turn(float turnDirection2)
    {
        float scaleTurn = _turnCannon.y + turnDirection2 * turnSpeed2 * Time.deltaTime;
        if (isClamp)
        {
            _turnCannon.y = Mathf.Clamp(scaleTurn, turnBarrelMin, turnBarrelMax);

        }
        else
        {
            _turnCannon.y = scaleTurn;
        }

        transform.localEulerAngles = _turnCannon;

        wheelRotation.x = _turnCannon.y;
        float scaleWheel = wheelRotation.x * wheelSpeed;
        wheelRotation2.x = scaleWheel;
        wheel1.localEulerAngles = -wheelRotation2;
        wheel2.localEulerAngles = wheelRotation2;
        wheel3.localEulerAngles = -wheelRotation2;
        wheel4.localEulerAngles = wheelRotation2;
    }

    public void Fire()
    {
        if(cdFire >= canFire&&countBulletMax>0)
        {
            var spawnBullet = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);

            Shot2 fireBullet = spawnBullet.GetComponent<Shot2>();

            fireBullet.Fly(firePoint2.forward, torQueForce);

            cdFire = 0;
            countBulletMax--;
            UpdateCountBullet();
        }

    }

    private void UpdateCountBullet()
    {
        countBullet.text = countBulletMax.ToString();
        if(countBulletMax <= 0)
        {
            noAmmo.SetActive(true);
        }
        else
        {
            noAmmo.SetActive(false);
        }
    }

    private void ReloadBullet()
    {
        if (countBulletMax<=0)
        {
            countBulletMax = 8;
        }
        UpdateCountBullet();
    }
}
