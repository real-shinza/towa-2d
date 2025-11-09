using Towa.Effect;
using Towa.UI.Game;
using UnityEngine;

namespace Towa.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController controller;
        [SerializeField]
        private PlayerAnimator animator;
        [SerializeField]
        private PlayerVoice voice;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filterDown, filterLeft, filterRight;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private ScoreManager scoreManager;
        [SerializeField]
        private TimeManager timeManager;
        [SerializeField]
        private PlayerHpManager playerHpManager;
        [SerializeField]
        private float moveSpeed, strikeSpeed, jumpForce, maxHp;
        [SerializeField]
        private float enemyDamage, attackDamage, straikeDamage;
        private float hp;
        private bool isGround, isDamage, isGoal;
        private PlayerState state;

        private void Start()
        {
            hp = maxHp;
            playerHpManager.SetGage(hp, maxHp);
            voice.Play("Start");
        }

        private void Update()
        {
            CheckDie();
            CheckGrounded();
            UpdateState();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Iblast"))
            {
                go.GetComponent<IblastManager>().DestroyTrigger();
                ApplyDamage(attackDamage);
            }
            else if (go.CompareTag("Campfire"))
            {
                isGoal = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (state == PlayerState.Win || state == PlayerState.Die)
                return;

            var go = other.gameObject;
            if (go.CompareTag("Enemy"))
                ApplyDamage(enemyDamage);
            else if (go.CompareTag("StrikingEnemy"))
                ApplyDamage(straikeDamage);
        }

        /// <summary>
        /// 落下、タイムアップはHPを0にする
        /// </summary>
        private void CheckDie()
        {
            if (transform.position.y < -5.5f || timeManager.RemainTime <= 0.0f)
                ApplyDamage(hp, true);
        }

        /// <summary>
        /// 地面に着いているか確認
        /// </summary>
        private void CheckGrounded()
        {
            if (rigidbody2d.IsTouching(filterDown))
            {
                isGround = true;
                animator.SetIsGround(true);

                if (state == PlayerState.JumpAttack)
                    ChangeState(PlayerState.None);
            }
            else
            {
                isGround = false;
                animator.SetIsGround(false);
            }
        }

        /// <summary>
        /// 状態更新
        /// </summary>
        private void UpdateState()
        {
            if (state != PlayerState.None &&
                state != PlayerState.Move &&
                state != PlayerState.Crouch)
                return;

            var nextState = GetNextState();
            ChangeState(nextState);
        }

        /// <summary>
        /// 状態変更
        /// </summary>
        /// <param name="nextState">変更後の状態</param>
        private void ChangeState(PlayerState nextState)
        {
            if (state == nextState)
                return;

            OnExit(state);
            state = nextState;
            OnEnter(state);
        }

        /// <summary>
        /// 次の状態を取得
        /// </summary>
        /// <returns>次の状態</returns>
        private PlayerState GetNextState()
        {
            // ゴール確認
            if (isGoal)
                return PlayerState.Win;

            // 死亡確認
            if (hp <= 0f)
                return PlayerState.Die;

            // 攻撃入力確認
            if (controller.GetInputAttack() && isGround)
                return PlayerState.Attack;
            if (controller.GetInputAttack() && !isGround)
                return PlayerState.JumpAttack;

            // ストライク入力確認
            if (controller.GetInputStrike())
                return PlayerState.Strike;

            // ブロック入力確認
            if (controller.GetInputBlock())
                return PlayerState.Block;

            // ダメージ確認
            if (isDamage)
                return PlayerState.Hurt;

            // 移動入力確認
            if (Mathf.Abs(controller.GetInputMove().x) >= 0.5f)
                return PlayerState.Move;

            // しゃがみ入力確認
            if (controller.GetInputMove().y <= -0.5f)
                return PlayerState.Crouch;

            return PlayerState.None;
        }

        private void OnEnter(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Move:
                    animator.SetIsMove(true);
                    break;
                case PlayerState.Attack:
                    animator.SetIsAttack(true);
                    voice.Play("Attack");
                    break;
                case PlayerState.JumpAttack:
                    animator.SetIsJumpAttack(true);
                    voice.Play("JumpAttack");
                    break;
                case PlayerState.Strike:
                    animator.SetIsStrike(true);
                    voice.Play("Strike");
                    break;
                case PlayerState.Block:
                    animator.SetIsBlock(true);
                    voice.Play("Block");
                    break;
                case PlayerState.Crouch:
                    animator.SetIsCrouch(true);
                    voice.Play("Crouch");
                    break;
                case PlayerState.Hurt:
                    animator.SetIsHurt(true);
                    voice.Play("Hurt");
                    break;
                case PlayerState.Win:
                    animator.WinTrigger();
                    voice.Play("Win");
                    scoreManager.Goal((int)timeManager.RemainTime);
                    gameManager.FinishGame();
                    break;
                case PlayerState.Die:
                    animator.DieTrigger();
                    voice.Play("Die");
                    gameManager.FinishGame();
                    break;
            }
        }

        private void OnExit(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Move:
                    animator.SetIsMove(false);
                    break;
                case PlayerState.Attack:
                    animator.SetIsAttack(false);
                    break;
                case PlayerState.JumpAttack:
                    animator.SetIsJumpAttack(false);
                    break;
                case PlayerState.Strike:
                    animator.SetIsStrike(false);
                    break;
                case PlayerState.Block:
                    animator.SetIsBlock(false);
                    break;
                case PlayerState.Crouch:
                    animator.SetIsCrouch(false);
                    break;
                case PlayerState.Hurt:
                    animator.SetIsHurt(false);
                    isDamage = false;
                    break;
            }
        }

        /// <summary>
        /// 行動処理
        /// </summary>
        private void UpdateMovement()
        {
            Move();
            Jump();
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        private void Move()
        {
            float moveVecX = 0f;

            if (state == PlayerState.Move || state == PlayerState.JumpAttack)
            {
                var moveX = controller.GetInputMove().x;
                moveVecX = (Mathf.Abs(moveX) >= 0.5f) ? Mathf.Sign(moveX) : 0f;

                // 移動方向に身体を向ける
                if (moveVecX >= 0f)
                    transform.localScale = new(1f, 1f, 1f);
                else
                    transform.localScale = new(-1f, 1f, 1f);
            }
            else if (state == PlayerState.Strike)
            {
                moveVecX = strikeSpeed / moveSpeed * transform.localScale.x;
            }

            // 移動方向に障害物があるなら
            if (rigidbody2d.IsTouching(filterLeft) && moveVecX < 0f ||
                rigidbody2d.IsTouching(filterRight) && moveVecX > 0f)
                moveVecX = 0f;

            rigidbody2d.velocity = new(moveVecX * moveSpeed, rigidbody2d.velocity.y);
        }

        /// <summary>
        /// ジャンプ処理
        /// </summary>
        private void Jump()
        {
            if (state != PlayerState.None &&
                state != PlayerState.Move &&
                state != PlayerState.Crouch)
                return;

            if (controller.GetInputJump() && isGround)
            {
                rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                voice.Play("Jump");
            }
        }

        /// <summary>
        /// ダメージ受けた処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        /// <param name="force">強制モード</param>
        private void ApplyDamage(float damage, bool isForce = false)
        {
            if (state != PlayerState.None &&
                state != PlayerState.Move &&
                state != PlayerState.Crouch &&
                !isForce)
                return;

            isDamage = true;
            hp -= damage;
            playerHpManager.SetGage(hp, maxHp);
        }

        /// <summary>
        /// ゴール時処理
        /// </summary>
        private void ApplyWin()
        {
            gameManager.ActiveResult();
        }

        /// <summary>
        /// 死亡時処理
        /// </summary>
        private void ApplyDie()
        {
            gameManager.ActiveResult();
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position + new Vector3(0f, -0.3f, 0f), Quaternion.identity);
        }
    }
}
