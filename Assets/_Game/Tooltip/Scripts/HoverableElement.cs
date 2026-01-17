using UnityEngine;

public class HoverableElement : MonoBehaviour, IHoverable
{
    [SerializeField] private ItemModel itemModel;

    public ItemModel GetItemModel()
    {
        return itemModel;
    }

    void IHoverable.OnHoverEnter() { }
    void IHoverable.OnHoverExit() { }
}
