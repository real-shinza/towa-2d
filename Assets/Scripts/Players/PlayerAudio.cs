using LanguageData;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private PlayerVoice playerVoice;
        [SerializeField]
        private Language language;
        [SerializeField]
        private Text lines;


        private void Update()
        {
            if (!audioSource.isPlaying && lines.enabled)
            {
                lines.enabled = false;
            }
        }
        
        
        
        public void PlayVoice(string id)
        {
            var audioClip = playerVoice.GetAudioClip(id);
            var text = playerVoice.GetText(id, language.LanguageType);
            var font = language.GetFont();

            if (audioClip == null || text == null)
                return;

            // ボイス音声再生
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();

            // ボイステキスト表示
            lines.text = text;
            lines.font = font;
            lines.enabled = true;
        }

        public bool GetIsPlaying()
        {
            return audioSource.isPlaying;
        }
    }
}