using Effect;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlayerAnimation playerAnimation;
        [SerializeField]
        private PlayerAudio playerAudio;
        [SerializeField]
        private Rigidbody2D rigidbody2d;
        [SerializeField]
        private ContactFilter2D filter2d;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private PlayerHpManager playerHpManager;
        [SerializeField]
        private float moveSpeed, strikeSpeed, jumpForce, maxHp;
        [SerializeField]
        private float enemyDamage, attackDamage, straikeDamage;

        private float moveVec, hp;
        private bool isGround, canJump, isFinish;
        private PlayerState state;



        public PlayerState State { get { return state; } set { state = value; } }



        private void Start()
        {
            hp = maxHp;
            playerHpManager.SetGage(hp, maxHp);
            playerAudio.PlayVoice("Start");
        }

        private void Update()
        {
            MovementUpdate();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherObj = other.gameObject;

            if (otherObj.tag == "Iblast")
            {
                otherObj.GetComponent<IblastManager>().DestroyTrigger();
                state = PlayerState.HURT;
                hp -= attackDamage;
                playerHpManager.SetGage(hp, maxHp);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherObj = other.gameObject;

            if (other.gameObject.tag == "Enemy")
            {
                state = PlayerState.HURT;
                hp -= enemyDamage;
                playerHpManager.SetGage(hp, maxHp);
            }
            else if (otherObj.tag == "StrikingEnemy")
            {
                state = PlayerState.HURT;
                hp -= straikeDamage;
                playerHpManager.SetGage(hp, maxHp);
            }
        }



        private void MovementUpdate()
        {
            moveVec = new float();

            if (!(state == PlayerState.WIN || state == PlayerState.DIE))
            {
                Ground();
                Jump();
                Move();
                Attack();
                JumpAttack();
                Strike();
                Block();
                Crouch();
                Hurt();
            }

            Die();

            // 実際の移動処理
            transform.Translate(moveVec * Time.deltaTime * moveSpeed, 0.0f, 0.0f);
        }

        private void Ground()
        {
            if (rigidbody2d.IsTouching(filter2d))
            {
                SetGround(true);
            }
            else
            {
                SetGround(false);
                canJump = false;
            }
        }

        private void Jump()
        {
            if (isGround)
                canJump = true;

            if (canJump && playerController.GetInputJump() && isGround && state != PlayerState.ATTACK)
            {
                canJump = false;
                rigidbody2d.AddForce(transform.up * jumpForce);
                playerAudio.PlayVoice("Jump");
            }
        }

        private void Move()
        {
            if (isGround && state != PlayerState.NONE && state != PlayerState.MOVE && state != PlayerState.CROUCH ||
                (!isGround && (state == PlayerState.STRIKE || state == PlayerState.BLOCK)))
                return;

            float moveVec = RoundOffValue(playerController.GetInputMove().x);

            // 横方向に入力があるなら
            if (moveVec != 0.0f)
            {
                SetMove(true);

                this.moveVec = moveVec;

                // 移動した方向に身体を向ける
                if (moveVec < 0.0f)
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                SetMove(false);
            }
        }

        private void Attack()
        {
            if (state == PlayerState.STRIKE || state == PlayerState.BLOCK || state == PlayerState.HURT)
                return;

            if (playerController.GetInputAttack() && isGround && state != PlayerState.ATTACK)
            {
                SetAttack(true);
                playerAudio.PlayVoice("Attack");
            }
        }

        private void JumpAttack()
        {
            if (state == PlayerState.STRIKE || state == PlayerState.BLOCK || state == PlayerState.HURT)
                return;

            if (playerController.GetInputAttack() && !isGround && state != PlayerState.JUMP_ATTACK)
            {
                SetJumpAttack(true);
                playerAudio.PlayVoice("JumpAttack");
            }
            else if (rigidbody2d.IsTouching(filter2d))
            {
                SetJumpAttack(false);
            }
        }

        private void Strike()
        {
            if (state == PlayerState.ATTACK || state == PlayerState.JUMP_ATTACK || state == PlayerState.BLOCK || state == PlayerState.HURT)
                return;

            if (playerController.GetInputStrike() && state != PlayerState.STRIKE)
            {
                SetStrike(true);
                playerAudio.PlayVoice("Strike");
            }

            if (state == PlayerState.STRIKE)
            {
                moveVec = strikeSpeed / moveSpeed * transform.localScale.x;
            }
        }

        private void Block()
        {
            if (state == PlayerState.ATTACK || state == PlayerState.JUMP_ATTACK || state == PlayerState.STRIKE || state == PlayerState.HURT)
                return;

            if (playerController.GetInputBlock() && state != PlayerState.BLOCK)
            {
                SetBlock(true);
                playerAudio.PlayVoice("Block");
            }
        }

        private void Crouch()
        {
            float moveVec = RoundOffValue(playerController.GetInputMove().y);

            if (moveVec <= -1.0f && isGround && (state == PlayerState.NONE || state == PlayerState.CROUCH))
            {
                if (state != PlayerState.CROUCH)
                    playerAudio.PlayVoice("Crouch");

                SetCrouch(true);
            }
            else
            {
                SetCrouch(false);
            }
        }

        private void Hurt()
        {
            if (state == PlayerState.HURT)
            {
                playerAudio.PlayVoice("Hurt");
                SetHurt(true);
            }
        }

        private void Die()
        {
            if (transform.position.y < -5.5f && state != PlayerState.DIE)
            {
                hp = 0f;
                playerHpManager.SetGage(hp, maxHp);
            }

            if (hp <= 0.0f && state != PlayerState.DIE)
            {
                playerAudio.PlayVoice("Die");
                SetState(true, PlayerState.DIE);
                playerAnimation.DieTrigger();
                playerAnimation.Speed(0.0f);
                gameManager.FinishGame();
            }
            else if (state == PlayerState.DIE && !playerAudio.GetIsPlaying() && !isFinish)
            {
                isFinish = true;
                gameManager.ActiveResult();
                playerAnimation.Speed(1.0f);
            }
        }

        private float RoundOffValue(float value)
        {
            if (value > 0.2f)
                return 1.0f;
            else if (value < -0.2f)
                return -1.0f;
            else
                return 0.0f;
        }

        private void SetGround(bool isGround)
        {
            this.isGround = isGround;
            playerAnimation.SetIsGround(isGround);
        }

        private void SetMove(bool isMove)
        {
            SetState(isMove, PlayerState.MOVE);
            playerAnimation.SetIsMove(isMove);
        }

        private void SetAttack(bool isAttack)
        {
            SetState(isAttack, PlayerState.ATTACK);
            playerAnimation.SetIsAttack(isAttack);
        }

        private void SetJumpAttack(bool isJumpAttack)
        {
            SetState(isJumpAttack, PlayerState.JUMP_ATTACK);
            playerAnimation.SetIsJumpAttack(isJumpAttack);
        }

        private void SetStrike(bool isStrike)
        {
            SetState(isStrike, PlayerState.STRIKE);
            playerAnimation.SetIsStrike(isStrike);
        }

        private void SetBlock(bool isBlock)
        {
            SetState(isBlock, PlayerState.BLOCK);
            playerAnimation.SetIsBlock(isBlock);
        }

        private void SetCrouch(bool isCrouch)
        {
            SetState(isCrouch, PlayerState.CROUCH);
            playerAnimation.IsCrouch(isCrouch);
        }

        private void SetHurt(bool isHurt)
        {
            SetState(isHurt, PlayerState.HURT);
            playerAnimation.IsHurt(isHurt);
        }

        private void SetState(bool isState, PlayerState state)
        {
            if (isState)
                this.state = state;
            else if (!isState && this.state == state)
                this.state = PlayerState.NONE;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position + new Vector3(0.0f, -0.3f, 0.0f), Quaternion.identity);
        }



        private void SetOffAttack() { SetAttack(false); }
        private void SetOffStrike() { SetStrike(false); }
        private void SetOffBlock() { SetBlock(false); }
        private void SetOffHurt() { SetHurt(false); }
    }
}
