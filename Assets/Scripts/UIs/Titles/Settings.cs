using LanguageData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private Language language;
        [SerializeField]
        private AudioSetting audioSetting;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private string[] settingsNames;
        [SerializeField]
        private Text settingsTitle;
        [SerializeField]
        private string[] languagesNames;
        [SerializeField]
        private Text languageTitle;
        [SerializeField]
        private Dropdown languageDropdown;
        [SerializeField]
        private string[] musicNames;
        [SerializeField]
        private Text musicTitle;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private string[] voiceNames;
        [SerializeField]
        private Text voiceTitle;
        [SerializeField]
        private Slider voiceSlider;



        public void ChangedLanguange()
        {
            InitLanguageDropdown();
            settingsTitle.text = settingsNames[(int)language.LanguageType];
            settingsTitle.font = language.GetFont();
            languageTitle.text = languagesNames[(int)language.LanguageType];
            languageTitle.font = language.GetFont();
            musicTitle.text = musicNames[(int)language.LanguageType];
            musicTitle.font = language.GetFont();
            voiceTitle.text = voiceNames[(int)language.LanguageType];
            voiceTitle.font = language.GetFont();
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

        private void InitLanguageDropdown()
        {
            language.LanguageType = (LanguageType)languageDropdown.value;
            languageDropdown.options.Clear();
            languageDropdown.options = GetOptions();
        }

        private List<Dropdown.OptionData> GetOptions()
        {
            var options = new List<Dropdown.OptionData>();
            var languageType = (int)language.LanguageType;

            for (int i = 0; i < language.GetLength(); i++)
            {
                string text;
                if (languageType == i)
                    text = language.GetName(languageType, i);
                else
                    text = $"{language.GetName(languageType, i)} ({language.GetName(i, i)})";

                var option = new Dropdown.OptionData();
                option.text = text;
                options.Add(option);
            }

            return options;
        }
    }
}
