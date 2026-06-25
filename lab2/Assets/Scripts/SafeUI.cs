using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; 

public class SafeUI : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject uiPanel;
    public TMP_Text displayText;
    public Animator doorAnimator;

    [Header("Safe Settings")]
    public string correctCode = "1234";
    public string openTriggerName = "Open";

    private string enteredCode = "";
    private bool doorOpened = false;

    [Header("Key Logic")]
    public GameObject keyObject;
    public Image keyIconPrefab;
    public Transform inventoryPanel;
    
    public InventoryItem keyItem;
    public EntranceDoorTrigger entranceDoorTrigger;

    public void EnterSafeUI()
    {
        if (doorOpened) return;

        InputBlocker.isBlocked = true;
        uiPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitSafeUI()
    {
        InputBlocker.isBlocked = false;
        uiPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PressNumber(string number)
    {
        if (doorOpened) return;

        if (enteredCode.Length < correctCode.Length)
        {
            enteredCode += number;
            displayText.text = enteredCode;
        }
    }

    public void ClearInput()
    {
        enteredCode = "";
        displayText.text = "";
    }

    public void SubmitCode()
    {
        if (doorOpened) return;

        if (enteredCode == correctCode)
        {
            doorOpened = true;
            doorAnimator.SetTrigger(openTriggerName);
            ExitSafeUI();

            StartCoroutine(CollectKeyAfterDelay());
        }
        else
        {
            enteredCode = "";
            displayText.text = "";
        }
    }

private IEnumerator CollectKeyAfterDelay()
{
    yield return new WaitForSeconds(2f);

    if (keyObject != null)
        Destroy(keyObject);

    if (keyItem != null)
        InventoryManager.Instance.AddItem(keyItem, showInUI: true);

    if (entranceDoorTrigger != null)
        entranceDoorTrigger.EnableDoorTrigger();
}

}
