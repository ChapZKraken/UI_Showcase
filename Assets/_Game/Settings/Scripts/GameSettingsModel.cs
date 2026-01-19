using UnityEngine;

public enum Mode { Hold, Toggle }

[CreateAssetMenu(fileName = "GameSettingsModel", menuName = "Settings/Game Settings Model")]
public class GameSettingsModel : ScriptableObject
{

    public float MouseSensitivity = 1.5f;
    public bool HeadBobEnabled = true;
    public bool InvertYAxis = false;
    public bool ShowCenterDot = true;
    public Mode SprintMode = Mode.Hold;
    public Mode CrouchMode = Mode.Hold;
    public float Brightness = 1f;
    public int Quality = 2;
}
