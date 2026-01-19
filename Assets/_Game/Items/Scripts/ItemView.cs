using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image itemImage;

    [SerializeField] private Vector3 initialPosition;
    private Vector3 initialScale;
    private Transform initialParent;

    private Tween moveTween;
    private Tween scaleTween;

    public Action OnStartDrag;

    private void Awake()
    {
        initialParent = transform.parent;
        initialPosition = rectTransform.anchoredPosition;
        initialScale = transform.localScale;
    }

    public void SetItemImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    public void SetPosition(Vector3 position)
    {
        moveTween.Kill();
        moveTween = rectTransform.DOAnchorPos(position, 0.2f).SetEase(Ease.OutQuad);
    }

    public void SetScale(bool up)
    {
        scaleTween.Kill();
        scaleTween = transform.DOScale(up ? initialScale * 1.5f : initialScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;

        transform.SetParent(initialParent);
        transform.position = Input.mousePosition;

        OnStartDrag?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;

        SetPosition(initialPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;

        transform.position = Input.mousePosition;
    }

    private void OnDestroy()
    {
        moveTween.Kill();
        scaleTween.Kill();
    }
}
