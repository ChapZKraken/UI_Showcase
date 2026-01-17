using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public enum Mode { Hold, Toggle }

    public float MouseSensitivity { get; private set; } = 1.5f;
    public bool HeadBobEnabled { get; private set; } = true;
    public bool InvertYAxis { get; private set; } = false;
    public bool ShowCenterDot { get; private set; } = true;
    public Mode SprintMode { get; private set; } = Mode.Hold;
    public Mode CrouchMode { get; private set; } = Mode.Hold;
    public float Brightness { get; private set; } = 1f;
    public int Quality { get; private set; } = 2;

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
        MouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void SetHeadBobEnabled(bool enabled)
    {
        HeadBobEnabled = enabled;
        PlayerPrefs.SetInt("HeadBobEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetInvertYAxis(bool invert)
    {
        InvertYAxis = invert;
        PlayerPrefs.SetInt("InvertYAxis", invert ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetShowCenterDot(bool enabled)
    {
        ShowCenterDot = enabled;
        PlayerPrefs.SetInt("ShowCenterDot", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSprintMode(Mode mode)
    {
        SprintMode = mode;
        PlayerPrefs.SetInt("SprintMode", (int)mode);
        PlayerPrefs.Save();
    }

    public void SetCrouchMode(Mode mode)
    {
        CrouchMode = mode;
        PlayerPrefs.SetInt("CrouchMode", (int)mode);
        PlayerPrefs.Save();
    }

    public void SetBrightness(float brightness)
    {
        Brightness = brightness;
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.Save();
    }

    public void SetQuality(int quality)
    {
        Quality = quality;
        PlayerPrefs.SetInt("Quality", quality);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.5f);
        HeadBobEnabled = PlayerPrefs.GetInt("HeadBobEnabled", 1) == 1;
        InvertYAxis = PlayerPrefs.GetInt("InvertYAxis", 0) == 1;
        ShowCenterDot = PlayerPrefs.GetInt("ShowCenterDot", 1) == 1;
        SprintMode = (Mode)PlayerPrefs.GetInt("SprintMode", 0);
        CrouchMode = (Mode)PlayerPrefs.GetInt("CrouchMode", 0);
        Brightness = PlayerPrefs.GetFloat("Brightness", 1f);
        Quality = PlayerPrefs.GetInt("Quality", 2);
    }
}
