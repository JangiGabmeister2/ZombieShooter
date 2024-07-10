using UnityEngine;

public enum MovementSpeed
{
    Walking = 0,
    Running = 1,
}

public class PlayerMovement : MonoBehaviour
{
    public KeyCode runKey = KeyCode.LeftShift;

    private MovementSpeed _mSpeed;

    private float _walkSpeed = 5f, _runSpeed = 10f, _moveSpeed = 0f;

    private float Vertical => Input.GetAxis("Vertical");
    private float Horiontal => Input.GetAxis("Horizontal");

    private void Update()
    {
        if (Vertical > 0)
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }

        if (Horiontal < 0)
        {
            transform.position += transform.right * -_moveSpeed * Time.deltaTime;
        }
        else if (Horiontal > 0)
        {
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(runKey))
        {
            SwitchMoveSpeed(true);
        }
        else
        {
            SwitchMoveSpeed(false);
        }
    }

    private void SwitchMoveSpeed(bool isRunning)
    {
        switch (_mSpeed)
        {
            case MovementSpeed.Walking:

                _moveSpeed = _walkSpeed;

                if (isRunning) _mSpeed = MovementSpeed.Running;
                break;
            case MovementSpeed.Running:

                _moveSpeed = _runSpeed;

                if (!isRunning) _mSpeed = MovementSpeed.Walking;
                break;
            default:
                break;
        }
    }
}
