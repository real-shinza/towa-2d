using Towa.Effect;
using UnityEngine;

namespace Towa.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyAnimator animator;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filterDown, filterLeft, filterRight;
        [SerializeField]
        private GameObject iblastPrefab;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private float moveSpeed, strikeSpeed;
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private float attackDistance, strikeDistance;
        [SerializeField]
        private float minCoolTime, maxCoolTime;
        [SerializeField]
        private float coolTime, moveVecX;
        private EnemyState state;
        private bool isAction, isDie;

        private void Update()
        {
            CheckDie();
            CheckGrounded();
            CountdownCoolTime();
            UpdateState();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Block"))
                moveVecX = 0f;
            else if (go.CompareTag("Sword"))
                isDie = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                moveVecX = 0f;
        }

        /// <summary>
        /// 落下したら削除
        /// </summary>
        private void CheckDie()
        {
            if (transform.position.y < -5.5f)
                Destroy(gameObject);
        }

        /// <summary>
        /// 地面に着いているか確認
        /// </summary>
        private void CheckGrounded()
        {
            if (rigidbody2d.IsTouching(filterDown))
                animator.SetIsGround(true);
            else
                animator.SetIsGround(false);
        }

        private void CountdownCoolTime()
        {
            if (coolTime >= 0f)
            {
                coolTime -= Time.deltaTime;
                if (coolTime <= 0f)
                {
                    coolTime = 0f;
                    isAction = false;
                }
            }
        }

        /// <summary>
        /// 状態更新
        /// </summary>
        private void UpdateState()
        {
            var nextState = GetNextState();
            ChangeState(nextState);
        }

        /// <summary>
        /// ステータス変更
        /// </summary>
        /// <param name="nextState">変更後のステータス</param>
        private void ChangeState(EnemyState nextState)
        {
            if (state == nextState)
                return;

            OnExit(state);
            state = nextState;
            OnEnter(state);
        }

        /// <summary>
        /// 次のステータスを取得
        /// </summary>
        /// <returns>次のステータス</returns>
        private EnemyState GetNextState()
        {
            // Playerが死亡した場合は何もしない
            if (!playerTransform)
                return EnemyState.Finish;

            // 死亡確認
            if (isDie)
                return EnemyState.Die;

            // アクション中なら、今の状態を返す
            if (isAction)
                return state;

            // 何も行動していない場合、次の行動を決める
            isAction = true;

            var distance = GetPlayerDistance();
            if (distance <= strikeDistance)
                return EnemyState.Strike;
            if (distance <= attackDistance)
                return EnemyState.Attack;

            SetCoolTime();
            if (Random.Range(0, 2) == 0)
                return EnemyState.None;
            else
                return EnemyState.Move;
        }

        private void SetCoolTime()
        {
            coolTime = Random.Range(minCoolTime, maxCoolTime);
        }

        private void OnEnter(EnemyState state)
        {
            switch (state)
            {
                case EnemyState.Move:
                    animator.SetIsMove(true);
                    SetMoveVecX();
                    break;
                case EnemyState.Attack:
                    animator.SetIsAttack(true);
                    SetPlayerVecX();
                    GenerateIblast();
                    break;
                case EnemyState.Strike:
                    animator.SetIsStrike(true);
                    SetPlayerVecX();
                    gameObject.tag = "StrikingEnemy";
                    break;
                case EnemyState.Die:
                    animator.DieTrigger();
                    gameObject.tag = "Untagged";
                    break;
                case EnemyState.Finish:
                    animator.SetIsMove(false);
                    animator.SetIsAttack(false);
                    animator.SetIsStrike(false);
                    break;
            }
        }

        private void OnExit(EnemyState state)
        {
            switch (state)
            {
                case EnemyState.Move:
                    animator.SetIsMove(false);
                    break;
                case EnemyState.Attack:
                    animator.SetIsAttack(false);
                    break;
                case EnemyState.Strike:
                    animator.SetIsStrike(false);
                    gameObject.tag = "Enemy";
                    break;
            }
        }

        /// <summary>
        /// 行動処理
        /// </summary>
        private void UpdateMovement()
        {
            if (state == EnemyState.Move)
            {
                // 移動方向に障害物があるなら
                if (rigidbody2d.IsTouching(filterLeft) && moveVecX < 0f ||
                    rigidbody2d.IsTouching(filterRight) && moveVecX > 0f)
                {
                    coolTime = 0.0f;
                    isAction = false;
                    ChangeState(EnemyState.None);
                    return;
                }
                rigidbody2d.velocity = new(moveVecX * moveSpeed, rigidbody2d.velocity.y);
            }
            else if (state == EnemyState.Strike)
            {
                rigidbody2d.velocity = new(moveVecX * strikeSpeed, rigidbody2d.velocity.y);
            }
            else
            {
                rigidbody2d.velocity = new(0f, rigidbody2d.velocity.y);
            }
        }

        /// <summary>
        /// プレイヤーとの距離を計算
        /// </summary>
        /// <returns>プレイヤーとの距離</returns>
        private float GetPlayerDistance()
        {
            var x = System.Math.Pow((double)transform.position.x - playerTransform.position.x, 2);
            var y = System.Math.Pow((double)transform.position.y - playerTransform.position.y, 2);
            return (float)System.Math.Sqrt(x + y);
        }

        /// <summary>
        /// プレイヤーの方向
        /// </summary>
        private void SetPlayerVecX()
        {
            moveVecX = Mathf.Sign(playerTransform.position.x - transform.position.x);
            transform.localScale = new(moveVecX, 1f, 1f);
        }

        /// <summary>
        /// 歩行向きをランダムで決める
        /// </summary>
        private void SetMoveVecX()
        {
            moveVecX = Mathf.Sign(Random.Range(-1f, 1f));
            transform.localScale = new(moveVecX, 1f, 1f);
        }

        /// <summary>
        /// 攻撃の炎を生成
        /// </summary>
        private void GenerateIblast()
        {
            var iblastObj = Instantiate(iblastPrefab, transform.position + new Vector3(0.75f * transform.localScale.x, -0.65f, 0f), Quaternion.identity);
            var iblast = iblastObj.GetComponent<IblastManager>();
            iblast.SetMoveVecX(transform.localScale.x);
            iblast.SetScaleX(transform.localScale.x);
        }

        /// <summary>
        /// アニメーション終了後処理
        /// </summary>
        private void EndAnimeAction()
        {
            ChangeState(EnemyState.None);
            SetCoolTime();
        }

        /// <summary>
        /// 死亡時処理
        /// </summary>
        private void ApplyDie()
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position + new Vector3(0f, -0.3f, 0f), Quaternion.identity);
        }

        /// <summary>
        /// ゲーム終了
        /// </summary>
        public void FinishGame()
        {
            state = EnemyState.Finish;
        }
    }
}
