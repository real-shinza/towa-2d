using Towa.Enemy;
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
            var otherObj = other.gameObject;

            if (otherObj.CompareTag("Enemy") || otherObj.CompareTag("StrikingEnemy"))
            {
                otherObj.GetComponent<EnemyController>().DestroyTrigger();
                scoreManager.KillEnemy();
            }
        }
    }
}
