using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Food[] _food;

	// Use this for initialization
	void Start ()
	{
	    _animator = GetComponent<Animator>();
	}
	
    public void StopRotating()
    {
        var arrowRotation = transform.rotation.eulerAngles;

        _animator.SetTrigger("CurveArrow");
        transform.eulerAngles = arrowRotation;
    }

    public void StopCurve()
    {
        var arrowScale = transform.localScale;

        transform.localScale = arrowScale;
        _animator.Stop();
        SpawnFood();
    }

    public void SpawnFood()
    {
        int randomIndex = Random.Range(0, _food.Length);
        var spawnedFood = Instantiate(_food[randomIndex]);

        spawnedFood.transform.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, transform.parent.localPosition.z);
        spawnedFood.Throw(transform.eulerAngles, transform.localScale);
    }

}
