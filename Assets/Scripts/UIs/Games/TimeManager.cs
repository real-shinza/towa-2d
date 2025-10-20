using System;
using UnityEngine;
using UnityEngine.UI;

namespace Towa.UI.Game
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private float maxTime;
        [SerializeField]
        private Text timeText;
        private float time;
        private bool isFinish;

        public float RemainTime { get { return time; } }
        public float ElapsedTime { get { return maxTime - time; } }

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

            if (time < 0.0f)
                time = 0.0f;

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
}
