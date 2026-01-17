using UnityEngine;

[CreateAssetMenu(fileName = "NewItemModel", menuName = "Inventory/Item Model")]
public class ItemModel : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemSprite;
}
