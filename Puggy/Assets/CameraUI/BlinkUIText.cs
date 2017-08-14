using UnityEngine;
using UnityEngine.UI;

namespace CameraUI
{
    public class BlinkUIText : BlinkUI
    {
        public override void TurnOffBlinking()
        {
            IsBlinking = false;
            GetComponent<Text>().enabled = false;
        }

        protected override void BlinkText()
        {
            Timer += Time.deltaTime;
            GetComponent<Text>().enabled = !(Timer > 0.5f);

            if (Timer > 1f)
            {
                Timer = 0;
            }
        }
    }
}
