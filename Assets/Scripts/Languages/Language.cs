using System;
using UnityEngine;

namespace LanguageData
{
    [CreateAssetMenu(fileName = "Language", menuName = "Databases/Language")]
    public class Language : ScriptableObject
    {
        [SerializeField]
        private LanguageType languageType;
        [SerializeField]
        private LanguageInfo[] languageInfo;



        public LanguageType LanguageType { get { return languageType; } set { languageType = value; } }
        public Font GetFont() { return languageInfo[(int)languageType].font; }



        [Serializable]
        public struct LanguageInfo
        {
            public Font font;
            public string[] names;
        }
    }



    public enum LanguageType
    {
        JAPANESE,
        ENGLISH,
        CHINESE_S,
        CHINESE_T,
        KOREAN,
        INDONESIAN,
    }
}
