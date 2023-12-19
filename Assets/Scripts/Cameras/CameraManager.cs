using UnityEngine;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;



        private void Update()
        {
            if (!playerTransform)
                return;

            float x, y, z = -10.0f;

            if (playerTransform.position.x < 0.0f)
                x = 0.0f;
            else
                x = playerTransform.position.x;

            if (playerTransform.position.y < 0.0f)
                y = 0.0f;
            else
                y = playerTransform.position.y;

            transform.position = new Vector3(x, y, z);
        }
    }
}
