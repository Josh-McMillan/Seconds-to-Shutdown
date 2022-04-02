using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    private Image clockImage;

    [SerializeField]
    private float refreshRate = 0.5f;

    [SerializeField]
    private Gradient coloredTime;

    private WaitForSeconds waitTime;

    private void Start()
    {
        clockImage = GetComponent<Image>();
        waitTime = new WaitForSeconds(refreshRate);
        SetClock(Clock.ScaledTimeLeft);
        StartCoroutine(OnClockTick());
    }

    private void SetClock(float timeLeft)
    {
        clockImage.fillAmount = timeLeft;
    }

    IEnumerator OnClockTick()
    {
        while (!Clock.GameOver)
        {
            clockImage.color = coloredTime.Evaluate(Clock.ScaledTimeLeft);
            SetClock(Clock.ScaledTimeLeft);
            yield return waitTime;
        }
    }
}
