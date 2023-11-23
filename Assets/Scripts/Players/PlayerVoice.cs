using LanguageData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerVoice", menuName = "Databases/PlayerVoice")]
    public class PlayerVoice : ScriptableObject
    {
        [SerializeField]
        private List<VoiceDatabases> voices;



        public AudioClip GetAudioClip(string id) { return voices.Find(v => v.id == id).audioClip; }
        public string GetText(string id, LanguageType languageType) { return voices.Find(v => v.id == id).texts[(int)languageType]; }



        [Serializable]
        public struct VoiceDatabases
        {
            public string id;
            public AudioClip audioClip;
            public string[] texts;
        }
    }
}
