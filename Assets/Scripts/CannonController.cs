using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonController : MonoBehaviour
{
    private SimpleControls _simpleControls;
    [SerializeField] protected Transform barrel;


    private void Awake()
    {
        _simpleControls = new SimpleControls();
        _simpleControls.gameplay.fire.performed += ctx => DoFire();
    }

    private void OnEnable()
    {
        _simpleControls.Enable();
    }

    private void OnDisable()
    {
        _simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = _simpleControls.gameplay.move.ReadValue<Vector2>();
        Debug.Log(move);
        Aim(move.y);
        
    }

    private void Aim(float aimDirection)
    {

    }

    private void DoFire()
    {
        Debug.Log("fire");
    }
}
