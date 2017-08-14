using UnityEngine;
using UnityEngine.UI;

namespace CameraUI
{
    public class BlinkUIImage : BlinkUI
    {
        public override void TurnOffBlinking()
        {
            IsBlinking = false;
            GetComponent<Image>().enabled = false;
        }

        protected override void BlinkText()
        {
            Timer += Time.deltaTime;
            GetComponent<Image>().enabled = !(Timer > 0.5f);

            if (Timer > 1f)
            {
                Timer = 0;
            }
        }
    }
}
