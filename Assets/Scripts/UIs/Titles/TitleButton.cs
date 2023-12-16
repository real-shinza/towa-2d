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
        private string[] controlNames;
        [SerializeField]
        private string[] creditsNames;
        [SerializeField]
        private Text playButtonText;
        [SerializeField]
        private Text controlButtonText;
        [SerializeField]
        private Text creditsButtonText;
        [SerializeField]
        private GameObject filter;
        [SerializeField]
        private GameObject controlObject;
        [SerializeField]
        private GameObject creditsObject;
        [SerializeField]
        private Language language;



        public void InitButton()
        {
            playButtonText.text = playNames[(int)language.LanguageType];
            playButtonText.font = language.GetFont();
            controlButtonText.text = controlNames[(int)language.LanguageType];
            controlButtonText.font = language.GetFont();
            creditsButtonText.text = creditsNames[(int)language.LanguageType];
            creditsButtonText.font = language.GetFont();
        }

        public void OnPlay()
        {
            SceneManager.LoadScene(1);
        }

        public void OnControl()
        {
            filter.SetActive(true);
            controlObject.SetActive(true);
        }

        public void OnCredits()
        {
            filter.SetActive(true);
            creditsObject.SetActive(true);
        }

        public void OnFilter()
        {
            filter.SetActive(false);
            controlObject.SetActive(false);
            creditsObject.SetActive(false);
        }
    }
}
