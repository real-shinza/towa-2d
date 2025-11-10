using Towa.Audio;
using UnityEngine;

namespace Towa.Effect
{
    public class ExplosionManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioParam audioParam;

        private void Awake()
        {
            audioSource.volume = audioParam.Se / 2;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
