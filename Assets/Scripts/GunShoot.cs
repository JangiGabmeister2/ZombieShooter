using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunShoot : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.Mouse0;

    public Transform gunTip;

    public bool isGunHolstered = true;

    private Vector3 _mousePosition = Vector3.zero;

    LineRenderer _lr => GetComponent<LineRenderer>();

    private void Start()
    {
        _lr.enabled = false;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            isGunHolstered = !isGunHolstered;
        }

        if (!isGunHolstered)
        {
            if (Input.GetKeyDown(shootKey))
            {
                if (Physics.Raycast(ray, out rayHit))
                {
                    _mousePosition = rayHit.point;
                }

                StartCoroutine("Shoot");
            }
        }
    }

    private IEnumerator Shoot()
    {
        _lr.enabled = true;
        _lr.positionCount = 2;
        _lr.SetPosition(0, gunTip.position);
        _lr.SetPosition(1, _mousePosition);

        yield return new WaitForSeconds(0.2f);

        _lr.enabled = false;
    }
}
