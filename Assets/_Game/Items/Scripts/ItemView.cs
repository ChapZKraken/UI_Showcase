using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image itemImage;

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private Tween moveTween;
    private Tween scaleTween;

    public Action OnStartDrag;

    private void Awake()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    public void SetItemImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    public void SetPosition(Vector3 position)
    {
        initialPosition = position;

        moveTween.Kill();
        moveTween = transform.DOMove(position, 0.2f).SetEase(Ease.OutQuad);
    }

    public void SetScale(bool up)
    {
        scaleTween.Kill();
        scaleTween = transform.DOScale(up ? initialScale * 1.5f : initialScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;

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
