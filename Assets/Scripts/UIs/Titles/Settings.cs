using Towa.Audio;
using Towa.Local;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Towa.Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private Dropdown languageDropdown;
        [SerializeField]
        private Slider bgmSlider;
        [SerializeField]
        private Slider seSlider;
        [SerializeField]
        private Slider voiceSlider;
        [SerializeField]
        private LocaleData localeData;
        [SerializeField]
        private AudioParam audioParam;
        [SerializeField]
        private AudioSource audioSource;

        public void ChangedLanguage()
        {
            LocalizationSettings.SelectedLocale = localeData.GetLocale(languageDropdown.value);
        }

        public void ChangedBgmSlider()
        {
            audioParam.Bgm = bgmSlider.value;
            audioSource.volume = bgmSlider.value / 10;
        }

        public void ChangedSeSlider()
        {
            audioParam.Se = seSlider.value;
        }

        public void ChangedVoiceSlider()
        {
            audioParam.Voice = voiceSlider.value;
        }
    }
}
