using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        StartTimer();
    }

    void Update()
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
    }

    private IEnumerator TimerCorutine()
    {
        start = DateTime.Now;
        while (true)
        {
            current = DateTime.Now;
            timer += current - start;
            start = current;
            timerText.text = timer.ToString(@"mm\:ss\.ff");
            yield return null;
        }
    }

    public void StartTimer()
    {
        timerCorutine = TimerCorutine();
        StartCoroutine(timerCorutine);
        state = TimerState.START;
    }

    public void StopTimer()
    {
        StopCoroutine(timerCorutine);
        timer = TimeSpan.Zero;
        state = TimerState.STOP;
    }
}
