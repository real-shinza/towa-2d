using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Title
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private AudioSetting audioSetting;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private Text settingsTitle;
        [SerializeField]
        private Text languageTitle;
        [SerializeField]
        private Dropdown languageDropdown;
        [SerializeField]
        private Language language;
        [SerializeField]
        private Text musicTitle;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Text voiceTitle;
        [SerializeField]
        private Slider voiceSlider;



        private void Awake()
        {
            ChangedLanguange();
        }



        public void ChangedLanguange()
        {
            LocalizationSettings.SelectedLocale = language.Data[languageDropdown.value].locale;
            languageDropdown.options.Clear();
            languageDropdown.options = GetOptions();
        }

        public void ChangedMusicSlider()
        {
            audioSetting.Music = musicSlider.value;
            audioSource.volume = musicSlider.value / 4;
        }

        public void ChangedVoiceSlider()
        {
            audioSetting.Voice = voiceSlider.value;
        }

        private List<Dropdown.OptionData> GetOptions()
        {
            var options = new List<Dropdown.OptionData>();

            foreach (var data in language.Data)
            {
                var option = new Dropdown.OptionData();
                option.text = data.localizedString.GetLocalizedString();
                options.Add(option);
            }

            return options;
        }
    }
}
