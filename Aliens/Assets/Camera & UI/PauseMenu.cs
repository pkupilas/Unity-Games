using Characters.Player;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private Player _player;
    private WeaponHud _weaponHud;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _weaponHud = FindObjectOfType<WeaponHud>();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.activeInHierarchy == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeToGame();
        }
	}

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _player.gameObject.SetActive(false);
        _weaponHud.enabled = false;
    }

    public void ResumeToGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _player.gameObject.SetActive(true);
        _weaponHud.enabled = true;
    }
}
