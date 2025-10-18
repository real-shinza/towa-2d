using UnityEngine;

namespace Enemy
{
    public class EnemiesManager : MonoBehaviour
    {
        private EnemyManager[] enemyManagers;

        void Start()
        {
            RegistChildren();
        }



        private void RegistChildren()
        {
            // 子オブジェクトを登録する
            enemyManagers = gameObject.GetComponentsInChildren<EnemyManager>();
        }

        public void FinishGame()
        {
            foreach (var enemyManager in enemyManagers)
            {
                enemyManager.GetIsFinish(true);
            }
        }
    }
}
