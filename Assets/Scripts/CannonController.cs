using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonController : MonoBehaviour
{
    private SimpleControls _simpleControls;
    [Header("AimBarrel")]
    [SerializeField] protected Transform barrel;
    [SerializeField] protected Vector2 _aimDirection = Vector2.zero;
    [SerializeField] protected float rotationSpeed = 10f;
    [SerializeField] protected float aimMaxRotation = 10f;
    [SerializeField] protected float aimMinRotation = -10f;

    [Header("TurnCannon")]
    [SerializeField] protected Vector2 _turnDirection = Vector2.zero;
    [SerializeField] protected float turnSpeed = 30f;
    [SerializeField] protected float turnMaxRotation = 30f;
    [SerializeField] protected float turnMinRotation = -30f;

    [Header("RollWheels")]
    [SerializeField] protected Vector2 _wheelRotation = Vector2.zero;
    [SerializeField] protected Transform wheelFrontRight;
    [SerializeField] protected Transform wheelRearRight;
    [SerializeField] protected Transform wheelFrontLeft;
    [SerializeField] protected Transform wheelRearLeft;


    [Header("Bullet")]
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float bulletSpeed = 50f;
    [SerializeField] protected float cdFire = 0f;
    [SerializeField] protected float timeFire = 2f;
    [SerializeField] protected float projectileForce = 3f;



    private void Awake()
    {
        _simpleControls = new SimpleControls();
        _turnDirection = transform.localEulerAngles;
        turnMaxRotation += _turnDirection.y;
        turnMinRotation += _turnDirection.y;
    }

    private void OnEnable()
    {
        _simpleControls.Enable(); 
        _simpleControls.gameplay.fire.performed += ctx => { DoFire(); };
        _simpleControls.gameplay.menu.performed += ctx => { MenuManager.Instance.Show(); };
    }

    private void OnDisable()
    {
        _simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = _simpleControls.gameplay.move.ReadValue<Vector2>();
        //Debug.Log(move);
        this.Aim(move.y);
        this.Turn(move.x);
        cdFire += Time.deltaTime;
    }

    private void Aim(float aimDirection)
    {

        float aimAmout =_aimDirection.x + aimDirection * rotationSpeed * Time.deltaTime;
        _aimDirection.x = Mathf.Clamp(aimAmout,aimMinRotation,aimMaxRotation);

        barrel.localEulerAngles = _aimDirection;
    }

    private void Turn(float turnDirection)
    {
        float turnAmout = _turnDirection.y + turnDirection * turnSpeed * Time.deltaTime;
        _turnDirection.y = Mathf.Clamp(turnAmout,turnMinRotation,turnMaxRotation);
        transform.localEulerAngles = _turnDirection;

        _wheelRotation.x = _turnDirection.y;
        wheelFrontLeft.localEulerAngles = _wheelRotation;
        wheelFrontRight.localEulerAngles = -_wheelRotation;
        wheelRearLeft.localEulerAngles = _wheelRotation;
        wheelRearRight.localEulerAngles = -_wheelRotation;
    }


    private void DoFire()
    {
        if(cdFire >= timeFire)
        {
            cdFire = 0;
            var spawnBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            var rbBullet = spawnBullet.GetComponent<Rigidbody>();
            rbBullet.AddTorque(Random.insideUnitSphere.normalized * projectileForce, ForceMode.Impulse);
            rbBullet.AddForce(firePoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }

    private void ShowMenu()
    {
        //MenuManager.Instance()
    }
}
