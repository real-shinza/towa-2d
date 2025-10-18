using UnityEngine;

namespace Towa.Audio
{
    [CreateAssetMenu(fileName = "AudioParam", menuName = "Settings/AudioParam")]
    public class AudioParam : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)]
        private float bgm;
        [SerializeField, Range(0f, 1f)]
        private float voice;

        public float Bgm { get { return bgm; } set { bgm = value; } }
        public float Voice { get { return voice; } set { voice = value; } }
    }
}
