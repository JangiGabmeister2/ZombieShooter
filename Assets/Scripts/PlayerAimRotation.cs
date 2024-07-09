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

        transform.LookAt(_mousePosition);
    }
}
