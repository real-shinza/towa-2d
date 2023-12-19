using LanguageData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Title
{
    public class TitleButton : MonoBehaviour
    {
        [SerializeField]
        private string[] playNames;
        [SerializeField]
        private string[] settingsNames;
        [SerializeField]
        private string[] creditsNames;
        [SerializeField]
        private Text playButtonText;
        [SerializeField]
        private Text settingsButtonText;
        [SerializeField]
        private Text creditsButtonText;
        [SerializeField]
        private GameObject filter;
        [SerializeField]
        private GameObject settingsObject;
        [SerializeField]
        private GameObject creditsObject;
        [SerializeField]
        private Language language;



        public void InitButton()
        {
            playButtonText.text = playNames[(int)language.LanguageType];
            playButtonText.font = language.GetFont();
            settingsButtonText.text = settingsNames[(int)language.LanguageType];
            settingsButtonText.font = language.GetFont();
            creditsButtonText.text = creditsNames[(int)language.LanguageType];
            creditsButtonText.font = language.GetFont();
        }

        public void OnPlay()
        {
            SceneManager.LoadScene(1);
        }

        public void OnSettings()
        {
            filter.SetActive(true);
            settingsObject.SetActive(true);
        }

        public void OnCredits()
        {
            filter.SetActive(true);
            creditsObject.SetActive(true);
        }

        public void OnFilter()
        {
            filter.SetActive(false);
            settingsObject.SetActive(false);
            creditsObject.SetActive(false);
        }
    }
}
