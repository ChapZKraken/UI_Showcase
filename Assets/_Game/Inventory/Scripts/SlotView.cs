using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotView : MonoBehaviour, IDropHandler
{
    public Action<ItemController> OnItemDropped;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        ItemController item = droppedObject.GetComponent<ItemController>();
        if (item == null)
            return;

        OnItemDropped?.Invoke(item);
    }
}
