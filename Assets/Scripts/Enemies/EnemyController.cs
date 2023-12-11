using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private float attackDistance;
        [SerializeField]
        private float strikeDistance;
        [SerializeField]
        private float minCoolTime;
        [SerializeField]
        private float maxCoolTime;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filterLeft, filterRight;

        private EnemyState state;
        private float coolTime = new float();
        private float moveDir;
        private bool isAction;
        private bool isDie;



        private void Update()
        {
            CountdownCoolTime();
            StateUpdate();
        }



        private void CountdownCoolTime()
        {
            if (coolTime > 0.0f)
            {
                coolTime -= Time.deltaTime;

                if (coolTime < 0.0f)
                {
                    coolTime = 0.0f;
                    isAction = false;
                }
            }
        }

        private void StateUpdate()
        {
            var nextState = GetNextState();

            if (state != nextState)
                state = nextState;
        }

        private EnemyState GetNextState()
        {
            // Playerが死亡した場合は何もしない
            if (!playerTransform)
                return EnemyState.NONE;

            // 敵自身が死亡した場合
            if (isDie)
                return EnemyState.DIE;

            // 歩行中壁に当たった場合、歩行を中断する
            if (state == EnemyState.MOVE &&
                ((rigidbody2d.IsTouching(filterLeft) && moveDir < 0.0f) ||
                (rigidbody2d.IsTouching(filterRight) && moveDir > 0.0f)))
                FinishAction();

            // 何かしら行動中なら、その状態を返す
            if (isAction)
                return state;

            // 何も行動していない場合、次の行動を決める
            isAction = true;

            if (GetDistance() <= strikeDistance)
                return EnemyState.STRIKE;
            else if (GetDistance() <= attackDistance)
                return EnemyState.ATTACK;
            else
                return GetStandby();
        }

        private float GetDistance()
        {
            // EnemyとPlayerの距離
            var x = Math.Pow((double)transform.position.x - playerTransform.position.x, 2);
            var y = Math.Pow((double)transform.position.y - playerTransform.position.y, 2);
            return (float)Math.Sqrt(x + y);
        }

        private EnemyState GetStandby()
        {
            coolTime = GetCoolTime();

            if (Random.Range(0, 2) == 0)
                return EnemyState.NONE;
            else
                return EnemyState.MOVE;
        }

        private float GetCoolTime()
        {
            return Random.Range(minCoolTime, maxCoolTime);
        }

        private void FinishAction()
        {
            state = EnemyState.NONE;
            coolTime = GetCoolTime();
        }



        public EnemyState GetState() { return state; }

        public float GetDirection()
        {
            if (playerTransform.position.x - transform.position.x >= 0.0f)
                return 1.0f;
            else
                return -1.0f;
        }

        public float GetMoveDirection()
        {
            if (Random.Range(-1.0f, 1.0f) >= 0.0f)
                moveDir = 1.0f;
            else
                moveDir = -1.0f;

            return moveDir;
        }

        public void DestroyTrigger()
        {
            gameObject.tag = "Untagged";
            isDie = true;
        }
    }
}