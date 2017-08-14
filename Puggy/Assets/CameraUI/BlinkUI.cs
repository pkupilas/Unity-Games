using UnityEngine;

namespace CameraUI
{
    public abstract class BlinkUI : MonoBehaviour
    {
        protected float Timer;
        protected bool IsBlinking;

        void Update()
        {
            if (IsBlinking)
            {
                BlinkText();
            }
        }

        protected abstract void BlinkText();

        public abstract void TurnOffBlinking();

        public void TurnOnBlinking()
        {
            IsBlinking = true;
        }
    }
}
