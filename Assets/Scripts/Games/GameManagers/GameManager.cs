using Enemy;
using Game;
using System;
using Towa.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using unityroom.Api;

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
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource voiceAudioSource;
    [SerializeField]
    private Towa.Audio.AudioParam audioSetting;



    public void Awake()
    {
        musicAudioSource.volume = audioSetting.Bgm / 4;
        voiceAudioSource.volume = audioSetting.Voice / 4;
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
        pause.SetActive(true);
    }

    public void OnTitle()
    {
        Time.timeScale = 1f;
        result.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void OnPlay()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }
}
