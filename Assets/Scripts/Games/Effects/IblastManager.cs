using UnityEngine;

namespace Towa.Effect
{
    public class IblastManager : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        private float moveVecX;

        void Update()
        {
            transform.Translate(moveVecX * Time.deltaTime * speed, 0.0f, 0.0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var go = other.gameObject;
            if (go.CompareTag("Tile"))
                DestroyTrigger();
        }

        private void OnBecameInvisible()
        {
            // 画面の外に出たら破壊
            DestroyTrigger();
        }

        public void DestroyTrigger()
        {
            gameObject.tag = "Untagged";
            Destroy(gameObject);
        }

        public void SetMoveVecX(float moveVecX) { this.moveVecX = moveVecX; }

        public void SetScaleX(float scaleX) { transform.localScale = new Vector3(scaleX, 1.0f, 1.0f); }
    }
}
