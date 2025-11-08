using Towa.Effect;
using UnityEngine;

namespace Towa.Enemy
{
    public class EnemyManagerOld : MonoBehaviour
    {
        [SerializeField]
        private EnemyController controller;
        [SerializeField]
        private EnemyAnimator animator;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filter2d;
        [SerializeField]
        private GameObject iblastPrefab;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private float moveSpeed;
        private float moveVec;
        private bool isFinish;
        private EnemyState beforeState;
        private bool firstStateTime;

        private void Start()
        {
            beforeState = controller.GetState();
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Block"))
                moveVec = 0f;
            // else if (go.CompareTag("Sword"))
            //     DestroyTrigger();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                moveVec = 0f;
            }
        }

        /// <summary>
        /// 行動処理
        /// </summary>
        private void UpdateMovement()
        {
            Ground();

            // もしゲーム終了していたら何もしない
            if (isFinish)
            {
                animator.SetIsMove(false);
                animator.SetIsAttack(false);
                animator.SetIsStrike(false);
                return;
            }

            // もしStateが変わっていたら
            if (beforeState != controller.GetState())
            {
                beforeState = controller.GetState();
                firstStateTime = true;
                animator.SetIsMove(false);
                animator.SetIsAttack(false);
                animator.SetIsStrike(false);
                gameObject.tag = "Enemy";
            }

            switch (controller.GetState())
            {
                case EnemyState.None:
                    Idle();
                    break;
                case EnemyState.Move:
                    Move();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Strike:
                    Strike();
                    break;
                case EnemyState.Die:
                    Die();
                    break;
            }

            // 実際に移動処理を行う
            transform.Translate(moveVec * Time.deltaTime * moveSpeed, 0.0f, 0.0f);
        }

        /// <summary>
        /// 地面着地処理
        /// </summary>
        private void Ground()
        {
            if (rigidbody2d.IsTouching(filter2d))
                SetGround(true);
            else
                SetGround(false);
        }

        /// <summary>
        /// 待機処理
        /// </summary>
        private void Idle()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = 0.0f;
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        private void Move()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = controller.GetMoveDirection();
            SetDirection(moveVec);
            animator.SetIsMove(true);
        }

        /// <summary>
        /// 攻撃処理
        /// </summary>
        private void Attack()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = 0.0f;
            SetDirection(controller.GetDirection());
            animator.SetIsAttack(true);
            GenerateIblast();
        }

        /// <summary>
        /// ストライク処理
        /// </summary>
        private void Strike()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = controller.GetDirection() * 1.5f;
            SetDirection(controller.GetDirection());
            animator.SetIsStrike(true);
            gameObject.tag = "StrikingEnemy";
        }

        /// <summary>
        /// 死亡処理
        /// </summary>
        private void Die()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveSpeed = 0.0f;
            animator.DieTrigger();
        }

        private void SetGround(bool isGround)
        {
            animator.SetIsGround(isGround);
        }

        private void SetDirection(float scaleX)
        {
            transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
        }

        private void GenerateIblast()
        {
            var iblastObj = Instantiate(iblastPrefab, transform.position + new Vector3(0.75f * transform.localScale.x, -0.65f, 0.0f), Quaternion.identity);
            var iblast = iblastObj.GetComponent<IblastManager>();
            iblast.SetMoveVecX(transform.localScale.x);
            iblast.SetScaleX(transform.localScale.x);
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position + new Vector3(0.0f, -0.3f, 0.0f), Quaternion.identity);
        }

        public void SetIsFinish(bool isFinish)
        {
            this.isFinish = isFinish;
        }
    }
}
