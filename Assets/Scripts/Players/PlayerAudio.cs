using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private Text lines;
        [SerializeField]
        private LocalizationTableCollection voiceTextTable;
        [SerializeField]
        private LocalizationTableCollection voiceAudioTable;


        private void Update()
        {
            if (!audioSource.isPlaying && lines.enabled)
            {
                lines.enabled = false;
            }
        }
        
        
        
        public void PlayVoice(string id)
        {
            var voiceText = new LocalizedString()
            {
                TableReference = voiceTextTable.SharedData.TableCollectionName,
                TableEntryReference = voiceTextTable.SharedData.GetEntry($"Voice/{id}").Id,
            };
            var voiceAudio = new LocalizedAudioClip()
            {
                TableReference = voiceAudioTable.SharedData.TableCollectionName,
                TableEntryReference = voiceAudioTable.SharedData.GetEntry($"Voice/{id}").Id,
            };

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