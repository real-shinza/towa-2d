using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        private Settings settings;
        [SerializeField]
        private Loge loge;
        [SerializeField]
        private TitleButton titleButton;
        [SerializeField]
        private Text version;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Slider voiceSlider;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioSetting audioSetting;



        private void Awake()
        {
            InitTitleUi();
            InitVersion();
            musicSlider.value = audioSetting.Music;
            voiceSlider.value = audioSetting.Voice;
            audioSource.volume = audioSetting.Music / 4;
        }



        public void ChangedLanguange()
        {
            InitTitleUi();
        }

        private void InitTitleUi()
        {
            settings.ChangedLanguange();
            loge.InitLogo();
            titleButton.InitButton();
        }

        private void InitVersion()
        {
            version.text = $"v {Application.version}";
        }
    }
}
