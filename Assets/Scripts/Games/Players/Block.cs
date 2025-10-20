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
            var otherObj = other.gameObject;

            if (otherObj.CompareTag("Iblast"))
            {
                otherObj.GetComponent<IblastManager>().DestroyTrigger();
                scoreManager.BlockIblast();
            }
            else if (otherObj.CompareTag("StrikingEnemy"))
            {
                scoreManager.BlockStrikingEnemy();
            }
        }
    }
}
