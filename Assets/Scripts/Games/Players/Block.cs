using Towa.Audio;
using Towa.Effect;
using Towa.UI.Game;
using UnityEngine;

namespace Towa.Player
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioParam audioParam;
        [SerializeField]
        private ScoreManager scoreManager;

        private void Awake()
        {
            audioSource.volume = audioParam.Se / 2;
        }

        private void OnEnable()
        {
            audioSource.Stop();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Iblast"))
            {
                go.GetComponent<IblastManager>().DestroyTrigger();
                scoreManager.BlockIblast();
                PlaySe();
            }
            else if (go.CompareTag("StrikingEnemy"))
            {
                scoreManager.BlockStrikingEnemy();
                PlaySe();
            }
        }

        private void PlaySe()
        {
            audioSource.time = 0f;
            audioSource.Play();
        }
    }
}
