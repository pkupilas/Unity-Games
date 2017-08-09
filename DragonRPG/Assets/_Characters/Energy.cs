using UnityEngine;
using UnityEngine.UI;
using _Camera;

public class Energy : MonoBehaviour
{
    [SerializeField] private RawImage _energyBar;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyCost;

    private float _currentEnergy;
    private CameraRaycaster _cameraRaycaster;

	void Start ()
	{
	    _currentEnergy = _maxEnergy;
	    _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        _cameraRaycaster.notifyRightClickObservers += OnRightClick;
    }

    private void OnRightClick(RaycastHit raycastHit, int layerHit)
    {
        float newCurrentEnergy = _currentEnergy - _energyCost;
        _currentEnergy = Mathf.Clamp(newCurrentEnergy, 0f, _maxEnergy);
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        float xValue = -(EnergyAsPercentage() / 2f) - 0.5f;
        _energyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }

    private float EnergyAsPercentage()
    {
        return _currentEnergy / _maxEnergy;
    }
}
