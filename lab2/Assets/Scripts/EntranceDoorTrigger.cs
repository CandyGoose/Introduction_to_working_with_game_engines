using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EntranceDoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    public string openTriggerName = "Open";

    public Collider triggerZone;

    public InventoryItem keyItem;

    private bool hasKey = false;
    private bool isOpened = false;

    void Start()
    {
        if (triggerZone != null)
            triggerZone.enabled = false;
    }

    public void EnableDoorTrigger()
    {
        hasKey = true;

        if (triggerZone != null)
            triggerZone.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened || !hasKey) return;

        if (other.CompareTag("Player"))
        {
            isOpened = true;
            doorAnimator.SetTrigger(openTriggerName);

            if (keyItem != null)
                InventoryManager.Instance.RemoveItem(keyItem);

            StartCoroutine(LoadMainMenuAfterDelay());
        }
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
