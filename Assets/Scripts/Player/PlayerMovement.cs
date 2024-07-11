using UnityEngine;

public enum MovementSpeed
{
    Walking = 0,
    Running = 1,
}

public class PlayerMovement : MonoBehaviour
{
    public KeyCode runKey = KeyCode.LeftShift;

    [SerializeField] AudioSource walkingSFX;

    private MovementSpeed _mSpeed;

    private float _walkSpeed = 5f, _runSpeed = 10f, _moveSpeed = 0f;

    private float Vertical => Input.GetAxis("Vertical");
    private float Horiontal => Input.GetAxis("Horizontal");

    private void Start()
    {
        walkingSFX.enabled = false;
    }

    private void Update()
    {
        if (Vertical > 0)
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;

            walkingSFX.enabled = true;
        }
        else
        {
            walkingSFX.enabled = false;
        }

        if (Horiontal < 0)
        {
            transform.position += transform.right * -_moveSpeed * Time.deltaTime;
        }
        else if (Horiontal > 0)
        {
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
        }

        SwitchMoveSpeed();
    }

    private void SwitchMoveSpeed()
    {
        _mSpeed = Input.GetKey(runKey) ? MovementSpeed.Running : MovementSpeed.Walking;

        switch (_mSpeed)
        {
            case MovementSpeed.Walking:

                _moveSpeed = _walkSpeed;

                walkingSFX.pitch = 1;

                break;
            case MovementSpeed.Running:

                _moveSpeed = _runSpeed;

                walkingSFX.pitch = 1.5f;

                break;
            default:
                break;
        }
    }
}
