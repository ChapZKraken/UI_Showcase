using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private ButtonSelection newGameButton;
    [SerializeField] private ButtonSelection settingsButton;
    [SerializeField] private ButtonSelection quitButton;

    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup settingsCanvasGroup;

    public enum Screen { Menu, Settings };
    private Screen currentScreen;

    public static Action<Screen> OnChangeScreen;

    private void Awake()
    {
        OnChangeScreen += ChangeScreen;

        newGameButton.AddListener(LoadGame);
        settingsButton.AddListener(() => ChangeScreen(Screen.Settings));
        quitButton.AddListener(Quit);
    }

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);

        ChangeScreen(Screen.Menu);
    }

    private void ChangeScreen(Screen screen)
    {
        currentScreen = screen;

        switch (currentScreen)
        {
            case Screen.Menu:
                ShowMenu(true);
                ShowSettings(false);
                break;

            case Screen.Settings:
                ShowSettings(true);
                ShowMenu(false);
                break;
        }
    }

    private void ShowMenu(bool enable)
    {
        menuCanvasGroup.alpha = enable ? 1 : 0;
        menuCanvasGroup.interactable = enable;
        menuCanvasGroup.blocksRaycasts = enable;
    }

    private void ShowSettings(bool enable)
    {
        settingsCanvasGroup.alpha = enable ? 1 : 0;
        settingsCanvasGroup.interactable = enable;
        settingsCanvasGroup.blocksRaycasts = enable;
    }

    private void LoadGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        OnChangeScreen -= ChangeScreen;

        newGameButton.RemoveListeners();
        settingsButton.RemoveListeners();
        quitButton.RemoveListeners();
    }
}
