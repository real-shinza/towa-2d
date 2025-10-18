using UnityEngine;
using UnityEngine.UI;

namespace Towa.UI.Title
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField]
        private Text versionText;

        private void Awake()
        {
            versionText.text = $"Ver {Application.version}";
        }
    }
}
