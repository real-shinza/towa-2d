using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerHpManager : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private float gageSpeed;
        private float gage, gageMemory;



        private void Update()
        {
            UpdateGage();
        }



        private void UpdateGage()
        {
            if (gage > gageMemory)
            {
                gage -= gageSpeed * Time.deltaTime;

                if (gage <= gageMemory)
                    gage = gageMemory;
            }
            else if (gage < gageMemory)
            {
                gage += gageSpeed * Time.deltaTime;

                if (gage >= gageMemory)
                    gage = gageMemory;
            }

            image.fillAmount = gage;
        }

        public void SetGage(float hp, float maxHp)
        {
            gageMemory = hp / maxHp;
        }
    }
}
