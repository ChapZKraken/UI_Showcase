using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button thisButton;

    [SerializeField] private TMP_Text mainText;
    [SerializeField] private RectTransform hoverEffect;

    private Vector3 initialScale;

    private Tween hoverTween;

    private void Awake()
    {
        initialScale = transform.localScale;

        ShowHoverEffect(false);
    }

    public void AddListener(UnityAction action)
    {
        thisButton.onClick.AddListener(action);
    }

    public void RemoveListeners()
    {
        thisButton.onClick.RemoveAllListeners();
    }

    public void Show(bool enable)
    {
        gameObject.SetActive(enable);
    }

    private void ShowHoverEffect(bool enable)
    {
        hoverTween.Kill();
        hoverTween = hoverEffect.DOScaleX(enable ? 1 : 0, 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowHoverEffect(true);

        transform.localScale = initialScale * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowHoverEffect(false);

        transform.localScale = initialScale;
    }

    private void OnDestroy()
    {
        hoverTween.Kill();
    }
}
