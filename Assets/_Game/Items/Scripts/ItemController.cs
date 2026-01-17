using System;
using UnityEngine;

public class ItemController : MonoBehaviour, IHoverable
{
    [SerializeField] private ItemView view;
    [SerializeField] private ItemModel model;

    public Action OnStartDrag;

    private void Awake()
    {
        view.OnStartDrag += StartDrag;
    }

    public void Start()
    {
        view.SetItemImage(model.ItemSprite);
    }

    public void SetPosition(Vector3 position)
    {
        view.SetPosition(position);
    }

    private void StartDrag()
    {
        OnStartDrag?.Invoke();
    }

    public ItemModel GetModel()
    {
        return model;
    }

    public ItemModel GetItemModel()
    {
        return model;
    }

    public void OnHoverEnter()
    {
        view.SetScale(true);
    }

    public void OnHoverExit()
    {
        view.SetScale(false);
    }

    private void OnDestroy()
    {
        view.OnStartDrag -= StartDrag;
    }
}
