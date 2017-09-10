using System.Linq;
using Characters.Player;
using UnityEngine;
using WorldObjects.Spawner;

public class PauseMenu : MonoBehaviour
{
    private Player _player;
    private WeaponHud _weaponHud;
    private EnemySpawner _enemySpawner;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _weaponHud = FindObjectOfType<WeaponHud>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPauseMenuEnabled())
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeToGame();
        }
	}

    private bool IsPauseMenuEnabled()
    {
        return transform.Cast<Transform>().Any(childTransform => childTransform.gameObject.activeInHierarchy);
    }

    private void SwitchPauseMenu(bool switchState)
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(switchState);
        }
    }

    private void Pause()
    {
        SwitchPauseMenu(true);
        Time.timeScale = 0;
        _player.gameObject.SetActive(false);
        _weaponHud.enabled = false;
        _enemySpawner.enabled = false;
    }

    public void ResumeToGame()
    {
        SwitchPauseMenu(false);
        Time.timeScale = 1;
        _player.gameObject.SetActive(true);
        _weaponHud.enabled = true;
        _enemySpawner.enabled = true;
    }
}
