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
        public string GetName(int languageType1, int languageType2)
        {
            return languageInfo[languageType1].names[languageType2];
        }
        public int GetLength() { return languageInfo.Length; }



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
