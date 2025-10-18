using UnityEngine;

namespace Effect
{
    public class ExplosionManager : MonoBehaviour
    {
        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
