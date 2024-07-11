using UnityEngine;

public class PlayerAimRotation : MonoBehaviour
{
    [SerializeField] GameObject mousePos;

    private Vector3 _mousePosition = Vector3.zero;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            _mousePosition = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z);
        }

        if (Vector3.Distance(transform.position, _mousePosition) > 2f)
        {
            transform.LookAt(_mousePosition);
        }

        mousePos.transform.position = _mousePosition;
    }
}
