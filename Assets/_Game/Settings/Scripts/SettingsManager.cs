using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SettingsTab { Game, Video, Audio };

public class SettingsManager : MonoBehaviour
{
    [Header("TABS")]
    [SerializeField] private List<Tab> tabs;

    [Header("BUTTONS")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button resetButton;

    [Header("GAME")]
    [SerializeField] private SliderUI mouseSensitivitySlider;
    [SerializeField] private Toggle headBobToggle;
    [SerializeField] private Toggle invertYAxisToggle;
    [SerializeField] private Toggle showCenterDotToggle;
    [SerializeField] private TMP_Dropdown sprintModeDropdown;
    [SerializeField] private TMP_Dropdown crouchModeDropdown;

    [Header("VIDEO")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private SliderUI brightnessSlider;

    [Header("AUDIO")]
    [SerializeField] private TMP_Dropdown localizationDropdown;
    [SerializeField] private SliderUI masterVolumeSlider;
    [SerializeField] private SliderUI ambienceVolumeSlider;
    [SerializeField] private SliderUI sfxVolumeSlider;
    [SerializeField] private SliderUI dialogueVolumeSlider;

    public static Action<SettingsTab> OnChangeTab;

    private void Awake()
    {
        backButton.onClick.AddListener(BackButtonClicked);
        resetButton.onClick.AddListener(ResetSettings);

        PopulateResolutions();
        PopulateQualityLevels();
        PopulateSprintMode();
        PopulateCrouchMode();

        SetAudioSliders();
        SetAudioSlidersListeners();
        SetSettingsSliders();
        SetSettingsSlidersListeners();

        ChangeTab(SettingsTab.Game);

        OnChangeTab += ChangeTab;
    }

    public void ChangeTab(SettingsTab newTab)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            Tab tab = tabs[i];
            tab.Select(tab.settingsTab == newTab);
        }
    }

    private void PopulateResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;

        List<string> resolutionNames = new List<string>();
        string res;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];
            res = resolution.width + " x " + resolution.height;
            resolutionNames.Add(res);

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionNames);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void PopulateQualityLevels()
    {
        List<string> options = QualitySettings.names.ToList();

        qualityDropdown.ClearOptions();

        qualityDropdown.AddOptions(options);
        qualityDropdown.value = GameSettings.Instance.Quality;
        qualityDropdown.RefreshShownValue();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        GameSettings.Instance.SetQuality(index);
    }

    private void PopulateSprintMode()
    {
        List<string> options = new List<string>() { "Hold", "Toggle" };

        sprintModeDropdown.ClearOptions();

        sprintModeDropdown.AddOptions(options);
        sprintModeDropdown.template.sizeDelta = new Vector2(sprintModeDropdown.template.sizeDelta.x, 50 * sprintModeDropdown.options.Count);
        sprintModeDropdown.value = (int)GameSettings.Instance.SprintMode;
        sprintModeDropdown.RefreshShownValue();
    }

    private void PopulateCrouchMode()
    {
        List<string> options = new List<string>() { "Hold", "Toggle" };

        crouchModeDropdown.ClearOptions();

        crouchModeDropdown.AddOptions(options);
        crouchModeDropdown.value = (int)GameSettings.Instance.CrouchMode;
        crouchModeDropdown.RefreshShownValue();
    }

    private void SetAudioSliders()
    {
        //masterVolumeSlider.SetValue(AudioManager.Instance.GetMasterVolume());
        //ambienceVolumeSlider.SetValue(AudioManager.Instance.GetAmbienceVolume());
        //sfxVolumeSlider.SetValue(AudioManager.Instance.GetSfxVolume());
        //dialogueVolumeSlider.SetValue(AudioManager.Instance.GetDialogueVolume());
    }

    private void SetAudioSlidersListeners()
    {
        //masterVolumeSlider.AddListener((float value) => AudioManager.Instance.SetMasterVolume(value));
        //ambienceVolumeSlider.AddListener((float value) => AudioManager.Instance.SetAmbienceVolume(value));
        //sfxVolumeSlider.AddListener((float value) => AudioManager.Instance.SetSfxVolume(value));
        //dialogueVolumeSlider.AddListener((float value) => AudioManager.Instance.SetDialogueVolume(value));
    }

    private void SetSettingsSliders()
    {
        mouseSensitivitySlider.SetValue(GameSettings.Instance.MouseSensitivity);
        headBobToggle.isOn = GameSettings.Instance.HeadBobEnabled;
        invertYAxisToggle.isOn = GameSettings.Instance.InvertYAxis;
        showCenterDotToggle.isOn = GameSettings.Instance.ShowCenterDot;
        sprintModeDropdown.value = (int)GameSettings.Instance.SprintMode;
        crouchModeDropdown.value = (int)GameSettings.Instance.CrouchMode;
        brightnessSlider.SetValue(GameSettings.Instance.Brightness);
    }

    private void SetSettingsSlidersListeners()
    {
        mouseSensitivitySlider.AddListener(GameSettings.Instance.SetMouseSensitivity);
        headBobToggle.onValueChanged.AddListener(GameSettings.Instance.SetHeadBobEnabled);
        invertYAxisToggle.onValueChanged.AddListener(GameSettings.Instance.SetInvertYAxis);
        showCenterDotToggle.onValueChanged.AddListener(GameSettings.Instance.SetShowCenterDot);
        sprintModeDropdown.onValueChanged.AddListener((int value) => GameSettings.Instance.SetSprintMode((GameSettings.Mode)value));
        crouchModeDropdown.onValueChanged.AddListener((int value) => GameSettings.Instance.SetCrouchMode((GameSettings.Mode)value));
        brightnessSlider.AddListener(GameSettings.Instance.SetBrightness);
    }

    private void BackButtonClicked()
    {
        MenuManager.OnChangeScreen?.Invoke(MenuManager.Screen.Menu);
    }

    private void ResetSettings()
    {
        GameSettings.Instance.SetMouseSensitivity(1.5f);
        GameSettings.Instance.SetHeadBobEnabled(true);
        GameSettings.Instance.SetInvertYAxis(false);
        GameSettings.Instance.SetShowCenterDot(true);
        GameSettings.Instance.SetSprintMode(GameSettings.Mode.Hold);
        GameSettings.Instance.SetCrouchMode(GameSettings.Mode.Hold);
        GameSettings.Instance.SetBrightness(1f);

        SetAudioSliders();
        SetSettingsSliders();
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();

        masterVolumeSlider.RemoveAllListeners();
        ambienceVolumeSlider.RemoveAllListeners();
        sfxVolumeSlider.RemoveAllListeners();
        dialogueVolumeSlider.RemoveAllListeners();

        mouseSensitivitySlider.RemoveAllListeners();
        headBobToggle.onValueChanged.RemoveAllListeners();
        invertYAxisToggle.onValueChanged.RemoveAllListeners();
        showCenterDotToggle.onValueChanged.RemoveAllListeners();
        sprintModeDropdown.onValueChanged.RemoveAllListeners();
        crouchModeDropdown.onValueChanged.RemoveAllListeners();
        brightnessSlider.RemoveAllListeners();

        OnChangeTab -= ChangeTab;
    }
}
