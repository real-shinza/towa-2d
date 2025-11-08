using Towa.Audio;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Towa.Player
{
    public class PlayerVoice : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private Text lines;
        [SerializeField]
        private AudioParam audioParam;

        private void Awake()
        {
            audioSource.volume = audioParam.Voice / 4;
        }

        private void Update()
        {
            if (!audioSource.isPlaying && lines.enabled)
            {
                lines.enabled = false;
            }
        }
        
        public void Play(string id)
        {
            var voiceText = new LocalizedString("VoiceText", $"Voice/{id}");
            var voiceAudio = new LocalizedAsset<AudioClip>();
            voiceAudio.SetReference("VoiceAudio", $"Voice/{id}");

            if (voiceText == null || voiceAudio == null)
                return;

            var text = voiceText.GetLocalizedString();
            var audioClip = voiceAudio.LoadAsset();

            if (text == null || audioClip == null)
                return;

            // ボイステキスト表示
            lines.text = text;
            lines.enabled = true;

            // ボイス音声再生
            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public bool GetIsPlaying()
        {
            return audioSource.isPlaying;
        }
    }
}
