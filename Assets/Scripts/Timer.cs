using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Timer : MonoBehaviour
{
    enum TimerState
    {
        START,
        STOP
    }
    private DateTime start;
    private DateTime current;
    private TimeSpan timer;
    private TimerState state;
    private IEnumerator timerCorutine;
    [SerializeField] private TextMeshProUGUI timerText;

    void Start()
    {
        StartCoroutine(TimerHandleCorutine());
    }

    private IEnumerator TimerHandleCorutine()
    {
        StartTimer();
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (state.Equals(TimerState.START))
                {
                    StopTimer();
                }
                else if (state.Equals(TimerState.STOP))
                {
                    StartTimer();
                }
            }
            yield return null;
        }
    }

    private IEnumerator TimerCorutine()
    {
        start = DateTime.Now;
        while (true)
        {
            current = DateTime.Now;
            timer += current - start;
            start = current;
            timerText.text = "Timer : " + timer.ToString(@"mm\:ss\.ff");
            yield return null;
        }
    }

    private void StartTimer()
    {
        timerCorutine = TimerCorutine();
        StartCoroutine(timerCorutine);
        state = TimerState.START;
    }

    private void StopTimer()
    {
        StopCoroutine(timerCorutine);
        state = TimerState.STOP;
    }
}
