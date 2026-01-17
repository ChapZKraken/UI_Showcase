using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TooltipView : MonoBehaviour
{
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text contentText;

    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private Ease animationEase;

    private RectTransform rectTransform;
    private Vector3 targetScale;
    private bool isVisible;

    private Tween scaleTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        targetScale = rectTransform.localScale;
    }

    private void Update()
    {
        if (!isVisible)
            return;

        FollowMouse();
    }

    public void SetHoverText(string header, string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            Hide();
            return;
        }

        headerText.text = header;
        contentText.text = content;
        Show();
    }

    private void Show()
    {
        isVisible = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = false;

        FollowMouse();
        Animate();
    }

    private void Hide()
    {
        isVisible = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    private void Animate()
    {
        rectTransform.localScale = Vector3.zero;

        scaleTween.Kill();
        scaleTween = rectTransform.DOScale(targetScale, animationDuration).SetEase(animationEase);
    }

    private void FollowMouse()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            canvasRect.GetComponent<Canvas>().worldCamera,
            out mousePos
        );

        rectTransform.anchoredPosition = mousePos + offset;

        ClampToCanvas();
    }

    private void ClampToCanvas()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 size = rectTransform.sizeDelta;
        Vector2 canvasSize = canvasRect.sizeDelta;

        float halfWidth = size.x / 2;
        float halfHeight = size.y / 2;

        pos.x = Mathf.Clamp(pos.x, -canvasSize.x / 2 + halfWidth, canvasSize.x / 2 - halfWidth);
        pos.y = Mathf.Clamp(pos.y, -canvasSize.y / 2 + halfHeight, canvasSize.y / 2 - halfHeight);

        rectTransform.anchoredPosition = pos;
    }

    private void OnDestroy()
    {
        scaleTween.Kill();
    }
}
