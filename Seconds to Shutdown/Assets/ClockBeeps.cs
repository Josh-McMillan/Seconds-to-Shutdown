using System.Collections;
using UnityEngine;

public class ClockBeeps : MonoBehaviour
{
    [SerializeField]
    private AudioClip beepSound;

    [SerializeField, Range(1.0f, 10.0f)]
    private float timeBetweenBeeps = 4.0f;

    private AudioSource audioSource;

    private WaitForSeconds waitTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waitTime = new WaitForSeconds(timeBetweenBeeps);
        StartCoroutine(TickBeep());
    }

    IEnumerator TickBeep()
    {
        while (!Clock.GameOver)
        {
            audioSource.PlayOneShot(beepSound);
            yield return waitTime;
        }
    }
}
