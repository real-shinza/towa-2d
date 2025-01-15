using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Title
{
    public class TitleButton : MonoBehaviour
    {
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
