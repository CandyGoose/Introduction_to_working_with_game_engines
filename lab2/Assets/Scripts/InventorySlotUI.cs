using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;

public void ClearSlot()
{
    icon.sprite = null;
    icon.enabled = false;
    countText.text = "";
    gameObject.SetActive(false);
}

public void SetItem(InventoryItem item, int count)
{
    gameObject.SetActive(true);
    icon.sprite = item.icon;
    icon.enabled = true;
    countText.text = count > 1 ? count.ToString() : "";
}

}
