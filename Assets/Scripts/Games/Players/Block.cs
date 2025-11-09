using Towa.Effect;
using Towa.UI.Game;
using UnityEngine;

namespace Towa.Player
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Iblast"))
            {
                go.GetComponent<IblastManager>().DestroyTrigger();
                scoreManager.BlockIblast();
            }
            else if (go.CompareTag("StrikingEnemy"))
            {
                scoreManager.BlockStrikingEnemy();
            }
        }
    }
}
