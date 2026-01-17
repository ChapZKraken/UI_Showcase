
using UnityEngine;
using static UnityEditor.Progress;

public class SlotController : MonoBehaviour
{
    [SerializeField] private SlotView view;

    private ItemController currentItem;

    private void Awake()
    {
        view.OnItemDropped += SetItem;
    }

    public void SetItem(ItemController item)
    {
        if (!IsEmpty())
            return;

        item.OnStartDrag += RemoveFromSlot;
        item.SetPosition(transform.position);

        currentItem = item;

        view.SetItemImage(currentItem.GetModel().ItemSprite);
    }

    private void RemoveFromSlot()
    {
        currentItem.OnStartDrag -= RemoveFromSlot;

        currentItem = null;
        view.SetItemImage(null);
    }

    private bool IsEmpty()
    {
        return currentItem == null;
    }

    private void OnDestroy()
    {
        view.OnItemDropped -= SetItem;
    }
}
