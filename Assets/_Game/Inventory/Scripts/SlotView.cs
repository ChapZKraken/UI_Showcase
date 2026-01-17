using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image itemImage;

    public Action<ItemController> OnItemDropped;

    public void SetItemImage(Sprite image)
    {
        itemImage.sprite = image;
        itemImage.enabled = image != null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        ItemController item = droppedObject.GetComponent<ItemController>();
        if (item == null)
            return;

        OnItemDropped?.Invoke(item);
    }
}
