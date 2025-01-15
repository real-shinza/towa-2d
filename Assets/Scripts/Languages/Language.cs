using System;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Language", menuName = "Databases/Language")]
public class Language : ScriptableObject
{
    [SerializeField]
    private LanguageData[] data;



    public LanguageData[] Data { get { return data; } }



    [Serializable]
    public struct LanguageData
    {
        public Locale locale;
        public LocalizedString localizedString;
    }
}
