using System;
using Towa.Enemy;
using Towa.UI.Game;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

namespace Towa
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private EnemiesManager enemiesManager;
        [SerializeField]
        private ScoreManager scoreManager;
        [SerializeField]
        private TimeManager timeManager;
        [SerializeField]
        private GameObject pause;
        [SerializeField]
        private GameObject result;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text timeText;

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        public void FinishGame()
        {
            timeManager.FinishGame();
            enemiesManager.FinishGame();
        }

        public void ActiveResult()
        {
            UnityroomApiClient.Instance.SendScore(1, scoreManager.TotalScore, ScoreboardWriteMode.Always);
            scoreText.text = $"{scoreManager.TotalScore:D8}";
            var timeSpan = TimeSpan.FromSeconds(timeManager.ElapsedTime);
            var minutes = timeSpan.Minutes;
            var seconds = timeSpan.Seconds;
            var milliseconds = timeSpan.Milliseconds;
            timeText.text = $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
            result.SetActive(true);
        }

        public void OnPause()
        {
            Time.timeScale = 0f;
        }

        public void OnResume()
        {
            Time.timeScale = 1f;
        }
    }
}
