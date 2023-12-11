using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private Text timeText;

    private float time;
    private bool isFinish;



    private void Start()
    {
        time = maxTime;
    }

    private void Update()
    {
        CountdownTimer();
    }



    private void CountdownTimer()
    {
        if (isFinish)
            return;

        time -= Time.deltaTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        var timeSpan = TimeSpan.FromSeconds(time);
        var minutes = timeSpan.Minutes;
        var seconds = timeSpan.Seconds;
        var milliseconds = timeSpan.Milliseconds;
        timeText.text = $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
    }



    public void FinishGame()
    {
        isFinish = true;
    }
}
