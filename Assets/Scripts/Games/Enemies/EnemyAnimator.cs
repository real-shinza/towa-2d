using UnityEngine;

namespace Towa.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private readonly int isGround = Animator.StringToHash("IsGround");
        private readonly int isMove = Animator.StringToHash("IsMove");
        private readonly int isAttack = Animator.StringToHash("IsAttack");
        private readonly int isStrike = Animator.StringToHash("IsStrike");
        private readonly int dieTrigger = Animator.StringToHash("DieTrigger");

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

        public void SetIsStrike(bool value)
        {
            animator.SetBool(isStrike, value);
        }

        public void DieTrigger()
        {
            animator.SetTrigger(dieTrigger);
        }
    }
}
