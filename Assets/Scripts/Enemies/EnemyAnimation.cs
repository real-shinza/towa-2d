using UnityEngine;

namespace Enemy
{
    public class EnemyAnimation : MonoBehaviour
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

        public void SetIsStrike(bool isStrike)
        {
            animator.SetBool("IsStrike", isStrike);
        }

        public void DieTrigger()
        {
            animator.SetTrigger("DieTrigger");
        }
    }
}
