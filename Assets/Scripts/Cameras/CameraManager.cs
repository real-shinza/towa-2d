using UnityEngine;

namespace Towa.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        private void Update()
        {
            if (!playerTransform)
                return;

            float x, y = 0f, z = -10f;

            if (playerTransform.position.x < 0f)
                x = 0f;
            else
                x = playerTransform.position.x;

            transform.position = new Vector3(x, y, z);
        }
    }
}
