using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        private LanguageDropdown languageDropdown;
        [SerializeField]
        private Loge loge;
        [SerializeField]
        private TitleButton titleButton;
        [SerializeField]
        private Text version;



        private void Awake()
        {
            InitTitleUi();
            InitVersion();
        }



        public void ChangedLanguange()
        {
            InitTitleUi();
        }

        private void InitTitleUi()
        {
            languageDropdown.InitLanguageDropdown();
            loge.InitLogo();
            titleButton.InitButton();
        }

        private void InitVersion()
        {
            version.text = $"v {Application.version}";
        }
    }
}
