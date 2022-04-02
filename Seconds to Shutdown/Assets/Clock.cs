using System;
using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip repairSound;

    [SerializeField]
    private AudioClip warningSound;

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private float finalDeathTimer = 3.0f;

    public static event Action GameIsOver;

    public static float TimeLeft { get; private set; } = 60.0f;

    public static bool OutOfTime { get; private set; } = false;

    public static bool GameOver { get; private set; } = false;

    public static float ScaledTimeLeft
    {
        get
        {
            return TimeLeft / 120.0f;
        }
    }

    private void OnEnable()
    {
        Repair.PerformedRepair += AddTime;
    }

    private void OnDisable()
    {
        Repair.PerformedRepair -= AddTime;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (OutOfTime) return;

        TimeLeft -= Time.deltaTime;

        if (TimeLeft > 0.0f) return;

        TimeLeft = 0.0f;
        OutOfTime = true;
        StartCoroutine(DeathTimer());
    }

    private void AddTime(float time)
    {
        if (GameOver) return;

        TimeLeft += time;
        audioSource.PlayOneShot(repairSound);
    }

    IEnumerator DeathTimer()
    {
        audioSource.PlayOneShot(warningSound);

        yield return new WaitForSeconds(finalDeathTimer);

        if (TimeLeft > 0)
        {
            StopAllCoroutines();
            OutOfTime = false;
        }

        audioSource.PlayOneShot(warningSound);

        yield return new WaitForSeconds(finalDeathTimer);

        if (TimeLeft <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            GameIsOver?.Invoke();
        }
        else
        {
            OutOfTime = false;
        }
    }
}
