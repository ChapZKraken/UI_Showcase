using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public SettingsTab settingsTab;

    private Button button;

    [SerializeField] private Image background;
    [SerializeField] private Image border;
    [SerializeField] private TMP_Text label;

    [SerializeField] private GameObject screen;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    public void Select(bool enable)
    {
        background.enabled = enable;
        border.color = enable ? Color.black : Color.white;
        label.color = enable ? Color.black : Color.white;
        label.fontStyle = enable ? FontStyles.Bold | FontStyles.UpperCase : FontStyles.Normal | FontStyles.UpperCase;

        ShowScreen(enable);
    }

    private void ShowScreen(bool enable)
    {
        screen.SetActive(enable);
    }

    private void OnClickButton()
    {
        SettingsManager.OnChangeTab?.Invoke(settingsTab);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClickButton);
    }
}
