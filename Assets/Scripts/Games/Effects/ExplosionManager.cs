using UnityEngine;

namespace Towa.Effect
{
    public class ExplosionManager : MonoBehaviour
    {
        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
