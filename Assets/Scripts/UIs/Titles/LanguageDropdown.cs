using LanguageData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class LanguageDropdown : MonoBehaviour
    {
        [SerializeField]
        private Language language;
        [SerializeField]
        private Dropdown dropdown;



        public void InitLanguageDropdown()
        {
            language.LanguageType = (LanguageType)dropdown.value;
            dropdown.options.Clear();
            dropdown.options = GetOptions();
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
