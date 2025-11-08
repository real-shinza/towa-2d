using Towa.UI.Game;
using UnityEngine;

namespace Towa.Player
{
    public class Sword : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Enemy") || go.CompareTag("StrikingEnemy"))
                scoreManager.KillEnemy();
        }
    }
}
