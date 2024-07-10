using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    private Vector3 _mousePosition = Vector3.zero;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            _mousePosition = rayHit.point;
        }

        if (Vector3.Distance(transform.position, _mousePosition) > 2f)
        {
            transform.LookAt(_mousePosition);
        }
    }
}
