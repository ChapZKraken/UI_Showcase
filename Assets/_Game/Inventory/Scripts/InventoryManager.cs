using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [Header("SLOTS")]
    [SerializeField] private SlotController slotPrefab;
    [SerializeField] private Transform slotsHolder;

    private void Start()
    {
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            Instantiate(slotPrefab, slotsHolder);
        }
    }
}
