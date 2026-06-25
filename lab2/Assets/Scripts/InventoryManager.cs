using System.Collections.Generic;
using UnityEngine;



public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
public int maxUniqueItems = 10; 

    public List<InventorySlotUI> slots = new();
    private List<InventoryEntry> inventory = new();
private void Awake()
{
    if (Instance == null) Instance = this;
    else Destroy(gameObject);

slots[1].ClearSlot();
    
}


public void AddItem(InventoryItem item, bool showInUI = true)
{
    Debug.Log($"[Inventory] AddItem: {item.name}");

    var existing = inventory.Find(e => e.item == item);

    if (existing != null)
    {
        if (existing.count < item.maxStack)
        {
            existing.count++;
        }
        else
        {
            Debug.LogWarning("Достигнут лимит стака для: " + item.name);
            return;
        }
    }
    else
    {
        if (inventory.Count >= maxUniqueItems)
        {
            Debug.LogWarning("Достигнут лимит уникальных предметов в инвентаре");
            return;
        }

        if (inventory.Count >= slots.Count)
        {
            Debug.LogWarning("Нет свободных слотов для предмета: " + item.name);
            return;
        }

        inventory.Add(new InventoryEntry(item, 1));
    }

    if (showInUI)
        UpdateUI();

    if (item.itemName == "Keys")
        FindObjectOfType<EntranceDoorTrigger>()?.EnableDoorTrigger();
}



public void RemoveItem(InventoryItem item, bool updateUI = true)
{
    var existing = inventory.Find(e => e.item == item);
    if (existing != null)
    {
        existing.count--;
        if (existing.count <= 0)
            inventory.Remove(existing);
    }

    if (updateUI)
        UpdateUI();
}

private void UpdateUI()
{

    var keyEntry = inventory.Find(e => e.item.itemName == "Keys");
    if (keyEntry != null)
    {
        slots[1].SetItem(keyEntry.item, keyEntry.count);
        slots[1].gameObject.SetActive(true);
    }
    else
    {
        slots[1].ClearSlot();
    }
}

}

[System.Serializable]
public class InventoryEntry
{
    public InventoryItem item;
    public int count;

    public InventoryEntry(InventoryItem item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
