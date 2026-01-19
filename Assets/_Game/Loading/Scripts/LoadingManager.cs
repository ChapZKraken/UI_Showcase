using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image loadingBar;

    private const float FadeDuration = 0.2f;

    private Tween fadeTween;

    private void Awake()
    {
        canvasGroup.alpha = 0;

        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(string sceneName, LoadSceneMode loadMode)
    {
        fadeTween.Kill();
        fadeTween = FadeIn();
        fadeTween.OnComplete(() =>
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, loadMode));
        });
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode loadMode)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadMode);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            loadingBar.fillAmount = operation.progress;

            if (operation.progress >= 0.9f)
            {
                loadingBar.fillAmount = 1f;

                yield return new WaitForSeconds(1f);

                operation.allowSceneActivation = true;

                FadeOut();
                yield break;
            }

            yield return null;
        }
    }

    private Tween FadeIn()
    {
        return canvasGroup.DOFade(1, FadeDuration);
    }

    private void FadeOut()
    {
        fadeTween.Kill();
        fadeTween = canvasGroup.DOFade(0, FadeDuration).OnComplete(DestroyLoading);
    }

    private void DestroyLoading()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        fadeTween.Kill();
    }
}
