using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Towa.Local
{
    [CreateAssetMenu(fileName = "LocaleData", menuName = "Settings/LocaleData")]
    public class LocaleData : ScriptableObject
    {
        [SerializeField]
        private Data[] datas;

        public Locale GetLocale(int index)
        {
            return datas[index].Locale;
        }

        [Serializable]
        public struct Data
        {
            [SerializeField]
            private Locale locale;
            [SerializeField]
            private LocalizedString localizedString;

            public Locale Locale { get { return locale; } }
            public LocalizedString LocalizedString { get { return localizedString; } }
        }
    }
}
