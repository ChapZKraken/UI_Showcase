using UnityEngine;

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
        item.transform.SetParent(transform);
        item.SetPosition(Vector3.zero);

        currentItem = item;
    }

    private void RemoveFromSlot()
    {
        currentItem.OnStartDrag -= RemoveFromSlot;

        currentItem = null;
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
