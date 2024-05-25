using UnityEngine;
using UnityEngine.Windows;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform cameraObj;

    private Vector2 inp;
    private float inpZ => InputManager.Instance.ZRoll;
    private float camSens => GameManager.Instance.SettingsObj.mouseSensitivity;

    // pitch = x axis (inp.y)
    // yaw = y axis (inp.x)
    // roll = z axis (inpZ)

    float yaw, pitch, roll;
    Vector3 values;

    void Update()
    {
        if (UIManager.Instance.isInGame)
        {
            inp = InputManager.Instance.LookVec;

            Quaternion pitchDelta = Quaternion.AngleAxis(-inp.y * camSens * Time.deltaTime, Vector3.right);
            Quaternion yawDelta = Quaternion.AngleAxis(inp.x * camSens * Time.deltaTime, Vector3.up);
            Quaternion rollDelta = Quaternion.AngleAxis(inpZ * camSens * Time.deltaTime, Vector3.forward);

            var rotDelta = pitchDelta * yawDelta * rollDelta;
            transform.rotation *= rotDelta;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.green);
        Debug.DrawRay(transform.position, Vector3.up * 2, Color.magenta);

        Debug.DrawRay(transform.position, transform.forward * 2, Color.blue);
        Debug.DrawRay(transform.position, Vector3.forward * 2, Color.cyan);

        Debug.DrawRay(transform.position, transform.right * 2, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * 2, Color.yellow);
    }
}
