using UnityEngine;

public enum SunPosition
{
    Dawn = 0,
    Noon = 1,
    Dusk = 2,
    Night = 3,
}

public enum SunDirection
{
    North = 0,
    South = 1,
    East = 2,
    West = 3,
}

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] private Transform _sun;

    [SerializeField] private SunPosition _sunPosition;
    [SerializeField] private SunDirection _sunDirection;

    private void Start()
    {
        switch (_sunPosition)
        {
            case SunPosition.Dawn:
                _sun.rotation = new Quaternion(0, _sun.rotation.y, _sun.rotation.z, 0);
                break;                         
            case SunPosition.Noon:             
                _sun.rotation = new Quaternion(90, _sun.rotation.y, _sun.rotation.z, 0);
                break;                        
            case SunPosition.Dusk:            
                _sun.rotation = new Quaternion(180, _sun.rotation.y, _sun.rotation.z, 0);
                break;                                          
            case SunPosition.Night:                             
                _sun.rotation = new Quaternion(-90, _sun.rotation.y, _sun.rotation.z, 0);
                break;
            default:
                break;
        }

        switch (_sunDirection)
        {
            case SunDirection.North:
                _sun.rotation = new Quaternion(_sun.rotation.x, 0, _sun.rotation.z, 0);
                break;
            case SunDirection.South:
                _sun.rotation = new Quaternion(_sun.rotation.x, 90, _sun.rotation.z, 0);
                break;
            case SunDirection.East:
                _sun.rotation = new Quaternion(_sun.rotation.x, 180, _sun.rotation.z, 0);
                break;
            case SunDirection.West:
                _sun.rotation = new Quaternion(_sun.rotation.x, -90, _sun.rotation.z, 0);
                break;
            default:
                break;
        }
    }
}
