using LanguageData;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class Loge : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] logos;
        [SerializeField]
        private Image logoImage;
        [SerializeField]
        private Language language;



        public void InitLogo()
        {
            logoImage.sprite = logos[(int)language.LanguageType];
        }
    }
}