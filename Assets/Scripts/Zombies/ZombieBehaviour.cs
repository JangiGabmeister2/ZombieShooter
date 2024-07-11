using UnityEngine;

public enum ZombieState
{
    Idle = 0,
    Follow = 1,
    Death = 2,
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Ragdollize))]
public class ZombieBehaviour : MonoBehaviour
{
    //zombie health
    [SerializeField] private int _health = 3;
    //the radius of the collider of the zombie where it checks whether or not the player is nearby
    [SerializeField] private float _searchRadius = 5f;
    //the speed at which the zombie moves (depends if walking or crawling)
    [SerializeField] private float _moveSpeed = 5f;

    //the current state of the zombie's behaviour
    private ZombieState _state;
    //reference to connected animator
    private Animator _zombieAnimator;
    //reference to the zombie's search collider
    private SphereCollider _searchCollider;
    //reference to script that ragdolls the zombie on death
    private Ragdollize _ragdoll;
    //reference to the player transform
    private Transform _player;

    private bool _isMoving = false;

    private void Start()
    {
        _zombieAnimator = GetComponent<Animator>();
        _ragdoll = GetComponent<Ragdollize>();

        _searchCollider = GetComponent<SphereCollider>();
        _searchCollider.radius = _searchRadius;
        _searchCollider.center = new Vector3(0, 1.8f, 0);
        _searchCollider.isTrigger = true;
    }

    private void Update()
    {
        SwitchZombieState();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }
    }

    #region Switch States
    private void SwitchZombieState()
    {
        switch (_state)
        {
            case ZombieState.Idle:
                IdleState();
                break;
            case ZombieState.Follow:
                FollowState();
                break;
            case ZombieState.Death:
                DeathState();
                break;
            default:
                break;
        }
    }

    private void IdleState()
    {
        _zombieAnimator.SetBool("isMoving", _isMoving);

        //check whether the player is close by, if it is, follow player
        if (_player != null)
        {
            _state = ZombieState.Follow;

            _isMoving = true;
        }

        CheckHealth();
    }

    private void FollowState()
    {
        //rotates zombie transform to 'look' at the player
        transform.LookAt(_player);

        //plays zombie walk animation upon following of player
        _zombieAnimator.SetBool("isMoving", _isMoving);

        //check whether the player is too far to follow, if it is, go back to idle state
        if (_player == null)
        {
            _state = ZombieState.Idle;

            _isMoving = false;
        }

        CheckHealth();
    }

    private void DeathState()
    {
        //play animation for death
        //but for not just disappear after a few secs

        _ragdoll.makeRagdoll = true;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }

    private void CheckHealth()
    {
        //check health - if its 0 or less, zombie dies
        if (_health <= 0)
        {
            _state = ZombieState.Death;
        }
    }
}
