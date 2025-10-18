using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;



        public void SetIsGround(bool isGround)
        {
            animator.SetBool("IsGround", isGround);
        }

        public void SetIsMove(bool isMove)
        {
            animator.SetBool("IsMove", isMove);
        }

        public void SetIsAttack(bool isAttack)
        {
            animator.SetBool("IsAttack", isAttack);
        }

        public void SetIsJumpAttack(bool isJumpAttack)
        {
            animator.SetBool("IsJumpAttack", isJumpAttack);
        }

        public void SetIsStrike(bool isStrike)
        {
            animator.SetBool("IsStrike", isStrike);
        }

        public void SetIsBlock(bool isBlock)
        {
            animator.SetBool("IsBlock", isBlock);
        }

        public void IsCrouch(bool isCrouch)
        {
            animator.SetBool("IsCrouch", isCrouch);
        }

        public void IsHurt(bool isHurt)
        {
            animator.SetBool("IsHurt", isHurt);
        }

        public void WinTrigger()
        {
            animator.SetTrigger("WinTrigger");
        }

        public void DieTrigger()
        {
            animator.SetTrigger("DieTrigger");
        }

        public void Speed(float speed)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}