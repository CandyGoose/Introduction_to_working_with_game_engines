using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections;

public class NPCSequence : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public Transform tvPoint;
    public Transform sofaPoint;

    public TMP_Text subtitleText;
    public GameObject triggerZone;

    public Renderer tvRenderer;
    public Material tvOnMaterial;
    public Material tvCodeMaterial;

    private bool triggered = false;

public AudioSource voiceSource;
public AudioClip talk1Clip;
public AudioClip talk2Clip;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Вошёл в триггер: " + other.name);

        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            triggerZone.GetComponent<Collider>().enabled = false;

            StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        animator.SetTrigger("Talk1");
        voiceSource.clip = talk1Clip;
        voiceSource.Play();
                subtitleText.text = "Опять телевизор не работает.";

        yield return new WaitForSeconds(11f);

        animator.SetTrigger("Repair");
        subtitleText.text = "Так, включаем...";
        yield return new WaitForSeconds(5f);
        tvRenderer.material = tvOnMaterial;
        yield return new WaitForSeconds(8f);
        tvRenderer.material = tvCodeMaterial;
        yield return new WaitForSeconds(3f);
                voiceSource.clip = talk2Clip;
        voiceSource.Play();

        animator.SetTrigger("Talk2");
        subtitleText.text = "Там, что-то странное, но хотя бы работает!";
        yield return new WaitForSeconds(11f);

        animator.SetTrigger("Finish");
        subtitleText.text = "";
        agent.SetDestination(sofaPoint.position);
        yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance + 0.1f);
    }
}
