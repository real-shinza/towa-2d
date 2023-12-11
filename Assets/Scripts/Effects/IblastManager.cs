using UnityEngine;

namespace Effect
{
    public class IblastManager : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private float moveVec;



        void Update()
        {
            transform.Translate(moveVec * Time.deltaTime * speed, 0.0f, 0.0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 壁や剣などに衝突したら破壊
            if (other.gameObject.tag == "Tile")
            {
                DestroyTrigger();
            }
        }

        void OnBecameInvisible()
        {
            // 画面の外に出たら破壊
            DestroyTrigger();
        }



        public void DestroyTrigger()
        {
            gameObject.tag = "Untagged";
            Destroy(gameObject);
        }

        public void SetMoveVec(float moveVec) { this.moveVec = moveVec; }

        public void SetScaleX(float scaleX) { transform.localScale = new Vector3(scaleX, 1.0f, 1.0f); }
    }
}
