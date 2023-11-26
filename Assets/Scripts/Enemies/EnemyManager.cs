using Effect;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyController enemyController;
        [SerializeField]
        private EnemyAnimation enemyAnimation;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filter2d;
        [SerializeField]
        private GameObject iblastPrefab;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private CapsuleCollider2D capsuleCollider;
        [SerializeField]
        private CircleCollider2D circleCollider;
        [SerializeField]
        private float moveSpeed;

        private float moveVec;
        private EnemyState beforeState;
        private bool firstStateTime;



        private void Start()
        {
            beforeState = enemyController.GetState();
        }

        private void Update()
        {
            MovementUpdate();
        }



        private void MovementUpdate()
        {
            Ground();

            // もしStateが変わっていたら
            if (beforeState != enemyController.GetState())
            {
                beforeState = enemyController.GetState();
                firstStateTime = true;
                enemyAnimation.SetIsMove(false);
                enemyAnimation.SetIsAttack(false);
                enemyAnimation.SetIsStrike(false);
                gameObject.tag = "Enemy";
            }

            switch (enemyController.GetState())
            {
                case EnemyState.NONE:
                    Idle();
                    break;
                case EnemyState.MOVE:
                    Move();
                    break;
                case EnemyState.ATTACK:
                    Attack();
                    break;
                case EnemyState.STRIKE:
                    Strike();
                    break;
                case EnemyState.DIE:
                    Die();
                    break;
            }

            // 実際に移動処理を行う
            transform.Translate(moveVec * Time.deltaTime * moveSpeed, 0.0f, 0.0f);
        }

        private void Ground()
        {
            if (rigidbody2d.IsTouching(filter2d))
                SetGround(true);
            else
                SetGround(false);
        }

        private void Idle()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = 0.0f;
        }

        private void Move()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = enemyController.GetMoveDirection();
            SetDirection(moveVec);
            enemyAnimation.SetIsMove(true);
        }

        private void Attack()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = 0.0f;
            SetDirection(enemyController.GetDirection());
            enemyAnimation.SetIsAttack(true);
            GenerateIblast();
        }

        private void Strike()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveVec = enemyController.GetDirection() * 1.5f;
            SetDirection(enemyController.GetDirection());
            enemyAnimation.SetIsStrike(true);
            gameObject.tag = "StrikingEnemy";
            capsuleCollider.enabled = false;
            circleCollider.enabled = true;
        }

        private void Die()
        {
            if (!firstStateTime)
                return;

            firstStateTime = false;
            moveSpeed = 0.0f;
            enemyAnimation.DieTrigger();
        }

        private void SetGround(bool isGround)
        {
            enemyAnimation.SetIsGround(isGround);
        }

        private void SetDirection(float scaleX)
        {
            transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
        }

        private void GenerateIblast()
        {
            var iblastObj = Instantiate(iblastPrefab, transform.position + new Vector3(0.75f * transform.localScale.x, -0.65f, 0.0f), Quaternion.identity);
            var iblast = iblastObj.GetComponent<IblastManager>();

            iblast.SetMoveVec(transform.localScale.x);
            iblast.SetScaleX(transform.localScale.x);
        }
        private void DestroyObject()
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position + new Vector3(0.0f, -0.3f, 0.0f), Quaternion.identity);
        }

        private void FinishStrike()
        {
            capsuleCollider.enabled = true;
            circleCollider.enabled = false;
        }

    }
}
