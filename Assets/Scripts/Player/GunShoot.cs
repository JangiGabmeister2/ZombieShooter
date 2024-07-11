using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunShoot : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse0;

    public Transform gunTip;

    public bool isGunHolstered = true;

    [SerializeField] private float _shotCooldown = .5f;

    private Vector3 _mousePosition = Vector3.zero;
    private float _shootCooldown;

    LineRenderer _lr => GetComponent<LineRenderer>();

    private void Start()
    {
        _lr.enabled = false;
        _lr.useWorldSpace = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            isGunHolstered = !isGunHolstered;
        }

        _shootCooldown -= Time.deltaTime;

        if (!isGunHolstered)
        {
            if (Input.GetKeyDown(shootKey) && _shootCooldown <= 0)
            {
                if (Physics.Raycast(ray, out rayHit))
                {
                    _mousePosition = rayHit.point;
                }

                StartCoroutine("Shoot");

                _shootCooldown = _shotCooldown;
            }
        }
    }

    private IEnumerator Shoot()
    {
        _lr.enabled = true;
        _lr.positionCount = 2;
        _lr.SetPosition(0, gunTip.position);
        _lr.SetPosition(1, _mousePosition);

        PlayGunShotSound();

        yield return new WaitForSeconds(0.1f);

        _lr.enabled = false;
    }

    private void PlayGunShotSound()
    {
        AudioClip[] shots = new AudioClip[] {
            SoundMaster.Instance.GetSoundClip("Gun Shot 1"),
            SoundMaster.Instance.GetSoundClip("Gun Shot 2"),
            SoundMaster.Instance.GetSoundClip("Gun Shot 3")};

        SoundMaster.Instance.PlaySFX(shots[Random.Range(0, shots.Length)]);
    }
}
