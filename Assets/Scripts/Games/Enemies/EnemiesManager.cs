using UnityEngine;

namespace Towa.Enemy
{
    public class EnemiesManager : MonoBehaviour
    {
        private EnemyManagerOld[] enemyManagers;

        void Start()
        {
            RegistChildren();
        }

        private void RegistChildren()
        {
            // 子オブジェクトを登録する
            enemyManagers = gameObject.GetComponentsInChildren<EnemyManagerOld>();
        }

        public void FinishGame()
        {
            foreach (var enemyManager in enemyManagers)
            {
                enemyManager.SetIsFinish(true);
            }
        }
    }
}
