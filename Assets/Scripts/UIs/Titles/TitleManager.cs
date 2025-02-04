using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        private Settings settings;
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
            version.text = $"Ver {Application.version}";
            musicSlider.value = audioSetting.Music;
            voiceSlider.value = audioSetting.Voice;
            audioSource.volume = audioSetting.Music / 4;
        }
    }
}
