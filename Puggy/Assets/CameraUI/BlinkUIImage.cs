using UnityEngine;
using UnityEngine.UI;

public class BlinkUIImage : BlinkUI
{

    private float _timer;
    private bool _isBlinking;

    void Update()
    {
        if (_isBlinking)
        {
            BlinkImage();
        }
    }

    private void BlinkImage()
    {
        _timer += Time.deltaTime;
        GetComponent<Image>().enabled = !(_timer > 0.5f);

        if (_timer > 1f)
        {
            _timer = 0;
        }
    }
    
    public override void TurnOnBlinking()
    {
        _isBlinking = true;
    }

    public override void TurnOffBlinking()
    {
        _isBlinking = false;
        GetComponent<Image>().enabled = false;
    }
}
