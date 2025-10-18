using UnityEngine;

namespace Towa.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioParam audioParam;

        private void Awake()
        {
            audioSource.volume = audioParam.Bgm / 4;
        }
    }
}
