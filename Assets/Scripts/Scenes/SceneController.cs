using UnityEngine;
using UnityEngine.SceneManagement;

namespace Towa.Scene
{
    public class SceneController : MonoBehaviour
    {
        public void OnLoadTitleScene()
        {
            SceneManager.LoadScene(0);
        }

        public void OnLoadGameScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}
