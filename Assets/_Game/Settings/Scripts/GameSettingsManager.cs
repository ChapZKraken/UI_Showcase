using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance;

    [SerializeField] private GameSettingsModel model;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        model.MouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public float GetMouseSensitivity()
    {
        return model.MouseSensitivity;
    }

    public void SetHeadBobEnabled(bool enabled)
    {
        model.HeadBobEnabled = enabled;
        PlayerPrefs.SetInt("HeadBobEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetHeadBobEnabled()
    {
        return model.HeadBobEnabled;
    }

    public void SetInvertYAxis(bool invert)
    {
        model.InvertYAxis = invert;
        PlayerPrefs.SetInt("InvertYAxis", invert ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetInvertYAxis()
    {
        return model.InvertYAxis;
    }

    public void SetShowCenterDot(bool enabled)
    {
        model.ShowCenterDot = enabled;
        PlayerPrefs.SetInt("ShowCenterDot", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetShowCenterDot()
    {
        return model.ShowCenterDot;
    }

    public void SetSprintMode(Mode mode)
    {
        model.SprintMode = mode;
        PlayerPrefs.SetInt("SprintMode", (int)mode);
        PlayerPrefs.Save();
    }

    public Mode GetSprintMode()
    {
        return model.SprintMode;
    }

    public void SetCrouchMode(Mode mode)
    {
        model.CrouchMode = mode;
        PlayerPrefs.SetInt("CrouchMode", (int)mode);
        PlayerPrefs.Save();
    }

    public Mode GetCrouchMode()
    {
        return model.CrouchMode;
    }

    public void SetBrightness(float brightness)
    {
        model.Brightness = brightness;
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.Save();
    }

    public float GetBrightness()
    {
        return model.Brightness;
    }

    public void SetQuality(int quality)
    {
        model.Quality = quality;
        PlayerPrefs.SetInt("Quality", quality);
        PlayerPrefs.Save();
    }

    public int GetQuality()
    {
        return model.Quality;
    }

    public void LoadSettings()
    {
        model.MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.5f);
        model.HeadBobEnabled = PlayerPrefs.GetInt("HeadBobEnabled", 1) == 1;
        model.InvertYAxis = PlayerPrefs.GetInt("InvertYAxis", 0) == 1;
        model.ShowCenterDot = PlayerPrefs.GetInt("ShowCenterDot", 1) == 1;
        model.SprintMode = (Mode)PlayerPrefs.GetInt("SprintMode", 0);
        model.CrouchMode = (Mode)PlayerPrefs.GetInt("CrouchMode", 0);
        model.Brightness = PlayerPrefs.GetFloat("Brightness", 1f);
        model.Quality = PlayerPrefs.GetInt("Quality", 2);
    }
}
