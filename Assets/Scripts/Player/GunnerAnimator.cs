using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GunnerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private float _zSpeed = 0f;
    private float _xSpeed = 0f;

    public KeyCode sprintKey = KeyCode.LeftAlt;
    public KeyCode unholsterKey = KeyCode.F;

    public float acceleration = 2f;
    public float deceleration = 2f;

    public float maxWalkSpeed = 0.5f;
    public float maxRunSpeed = 2;

    private int _xSpeedHash;
    private int _zSpeedHash;

    bool _aimPress = false;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        _xSpeedHash = Animator.StringToHash("movementSpeedX");
        _zSpeedHash = Animator.StringToHash("movementSpeedZ");
    }

    private void Update()
    {
        bool _forwardPress = Input.GetAxisRaw("Vertical") > 0; //forward button - default W
        bool _leftPress = Input.GetAxisRaw("Horizontal") < 0; //left button - default A
        bool _rightPress = Input.GetAxisRaw("Horizontal") > 0; //right button - default D
        bool _runPress = Input.GetKey(sprintKey);

        float currentMaxSpeed = _runPress ? maxRunSpeed : maxWalkSpeed;

        ChangeSpeed(_forwardPress, _leftPress, _rightPress, _runPress, currentMaxSpeed);
        LockOrResetSpeed(_forwardPress, _leftPress, _rightPress, _runPress, currentMaxSpeed);

        _playerAnimator.SetFloat(_zSpeedHash, _zSpeed);
        _playerAnimator.SetFloat(_xSpeedHash, _xSpeed);

        if (Input.GetKeyDown(unholsterKey))
        {
            _aimPress = !_aimPress;
        }

        _playerAnimator.SetBool("isAiming", _aimPress);
    }

    private void ChangeSpeed(bool _forwardPress, bool _leftPress, bool _rightPress, bool _runPress, float currentMaxSpeed)
    {
        #region Forward Movement
        //if player presses forward button, accelerates upto current max speed
        if (_forwardPress && _zSpeed < currentMaxSpeed)
        {
            _zSpeed += Time.deltaTime * acceleration;
        }
        //if player is not pressing forward button, decelerates to 0
        else if (!_forwardPress && _zSpeed > 0)
        {
            _zSpeed -= Time.deltaTime * deceleration;
        }
        #endregion

        #region Left Movement
        //if player presses left button, accelerates 
        if (_leftPress && _xSpeed > -currentMaxSpeed)
        {
            _xSpeed -= Time.deltaTime * acceleration;
        }
        else if (!_leftPress && _xSpeed < 0)
        {
            _xSpeed += Time.deltaTime * deceleration;
        }
        #endregion

        #region Right Movement
        //if player presses right button, accelerates
        if (_rightPress && _xSpeed < currentMaxSpeed)
        {
            _xSpeed += Time.deltaTime * acceleration;
        }
        else if (!_rightPress && _xSpeed > 0)
        {
            _xSpeed -= Time.deltaTime * deceleration;
        }
        #endregion
    }

    private void LockOrResetSpeed(bool _forwardPress, bool _leftPress, bool _rightPress, bool _runPress, float currentMaxSpeed)
    {
        #region Forward Movement
        //if player is not pressing forward button and movement speed is below 0, sets it to 0
        if (!_forwardPress && _zSpeed < 0)
        {
            _zSpeed = 0;
        }
        //lock forward run speed to max run speed
        if (_forwardPress && _runPress && _zSpeed > currentMaxSpeed)
        {
            _zSpeed = currentMaxSpeed;
        }
        //decelerate to the max walk speed
        else if (_forwardPress && _zSpeed > currentMaxSpeed)
        {
            _zSpeed -= Time.deltaTime * deceleration;

            //round to current max speed if within offset
            if (_zSpeed > currentMaxSpeed && _zSpeed < (currentMaxSpeed + 0.05f))
            {
                _zSpeed = currentMaxSpeed;
            }
        }
        //round to current max speed if within offset
        else if (_forwardPress && _zSpeed < currentMaxSpeed && _zSpeed > (currentMaxSpeed - 0.05f))
        {
            _zSpeed = currentMaxSpeed;
        }
        #endregion

        #region Left and Right Movement

        #region Left Movement
        //lock left run speed to max run speed
        if (_leftPress && _runPress && _xSpeed < -currentMaxSpeed)
        {
            _xSpeed = -currentMaxSpeed;
        }
        //decelerate to the max walk speed
        else if (_leftPress && _xSpeed < -currentMaxSpeed)
        {
            _xSpeed += Time.deltaTime * deceleration;

            //round to current max speed if within offset
            if (_xSpeed < -currentMaxSpeed && _xSpeed > (-currentMaxSpeed - 0.05f))
            {
                _xSpeed = -currentMaxSpeed;
            }
        }
        //round to current max speed if within offset
        else if (_leftPress && _xSpeed > -currentMaxSpeed && _xSpeed < (-currentMaxSpeed + 0.05f))
        {
            _xSpeed = -currentMaxSpeed;
        }
        #endregion

        #region Right Movement
        //lock right run speed to max run speed
        if (_rightPress && _runPress && _xSpeed > currentMaxSpeed)
        {
            _xSpeed = currentMaxSpeed;
        }
        //decelerate to the max walk speed
        else if (_rightPress && _xSpeed > currentMaxSpeed)
        {
            _xSpeed -= Time.deltaTime * deceleration;

            //round to current max speed if within offset
            if (_xSpeed > currentMaxSpeed && _xSpeed < (currentMaxSpeed + 0.05f))
            {
                _xSpeed = currentMaxSpeed;
            }
        }
        //round to current max speed if within offset
        else if (_rightPress && _xSpeed < currentMaxSpeed && _xSpeed > (currentMaxSpeed - 0.05f))
        {
            _xSpeed = currentMaxSpeed;
        }
        #endregion

        //if player is neither pressing left or right buttons, sets left and right movement speeds to 0
        if (!_leftPress && !_rightPress && _xSpeed != 0 && _xSpeed > -0.05f && _xSpeed < 0.05f)
        {
            _xSpeed = 0;
        }
        #endregion
    }
}
