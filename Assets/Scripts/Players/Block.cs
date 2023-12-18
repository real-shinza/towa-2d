using Effect;
using Game;
using UnityEngine;

namespace Player
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private ScoreManager scoreManager;



        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;

            if (otherObj.tag == "Iblast")
            {
                otherObj.GetComponent<IblastManager>().DestroyTrigger();
                scoreManager.BlockIblast();
            }
            else if (otherObj.tag == "StrikingEnemy")
            {
                scoreManager.BlockStrikingEnemy();
            }
        }
    }
}
