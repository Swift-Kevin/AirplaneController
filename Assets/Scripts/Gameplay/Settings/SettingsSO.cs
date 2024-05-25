using UnityEngine;

[CreateAssetMenu(fileName = "Settings Object", menuName = "Settings")]
public class SettingsSO : ScriptableObject
{
    // Audios
    public float masterVol;

    [Seperator]
    // Other
    public float mouseSensitivity;
}
