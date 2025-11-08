using UnityEngine;

namespace Towa.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private readonly int isGround = Animator.StringToHash("IsGround");
        private readonly int isMove = Animator.StringToHash("IsMove");
        private readonly int isAttack = Animator.StringToHash("IsAttack");
        private readonly int isJumpAttack = Animator.StringToHash("IsJumpAttack");
        private readonly int isStrike = Animator.StringToHash("IsStrike");
        private readonly int isBlock = Animator.StringToHash("IsBlock");
        private readonly int isCrouch = Animator.StringToHash("IsCrouch");
        private readonly int isHurt = Animator.StringToHash("IsHurt");
        private readonly int winTrigger = Animator.StringToHash("WinTrigger");
        private readonly int dieTrigger = Animator.StringToHash("DieTrigger");
        private readonly int speed = Animator.StringToHash("Speed");


        public void SetIsGround(bool value)
        {
            animator.SetBool(isGround, value);
        }

        public void SetIsMove(bool value)
        {
            animator.SetBool(isMove, value);
        }

        public void SetIsAttack(bool value)
        {
            animator.SetBool(isAttack, value);
        }

        public void SetIsJumpAttack(bool value)
        {
            animator.SetBool(isJumpAttack, value);
        }

        public void SetIsStrike(bool value)
        {
            animator.SetBool(isStrike, value);
        }

        public void SetIsBlock(bool value)
        {
            animator.SetBool(isBlock, value);
        }

        public void SetIsCrouch(bool value)
        {
            animator.SetBool(isCrouch, value);
        }

        public void SetIsHurt(bool value)
        {
            animator.SetBool(isHurt, value);
        }

        public void WinTrigger()
        {
            animator.SetTrigger(winTrigger);
        }

        public void DieTrigger()
        {
            animator.SetTrigger(dieTrigger);
        }

        public void Speed(float value)
        {
            animator.SetFloat(speed, value);
        }
    }
}
