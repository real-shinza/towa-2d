using Enemy;
using UnityEngine;

namespace Player
{
    public class Sword : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;

            if (otherObj.tag == "Enemy" || otherObj.tag == "StrikingEnemy")
            {
                otherObj.GetComponent<EnemyController>().DestroyTrigger();
                scoreManager.KillEnemy();
            }
        }
    }
}
