using UnityEngine;

public class SimpleTurret : MonoBehaviour
{
    [SerializeField] private GameObject _ammunition;
    [SerializeField] private GameObject _spawnPoint;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        GameObject projectile = Instantiate(_ammunition,_spawnPoint.transform);
    }
}
