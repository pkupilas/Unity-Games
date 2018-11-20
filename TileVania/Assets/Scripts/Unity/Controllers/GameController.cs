using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Systems _Systems;
    private Contexts _Contexts;
    public static GameController Instance = null;

    [SerializeField]
    private int PlayerLives = 3;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _Contexts = Contexts.sharedInstance;
        _Systems = CreateSystems(_Contexts);
    }

    private void Start()
    {
        _Systems.Initialize();
        PlayerView.OnDead += ProcessPlayerDeath;
    }

    private void Update()
    {
        _Systems.Execute();
        _Systems.Cleanup();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Systems();
    }

    private void OnDestroy()
    {
        _Systems.TearDown();
    }

    private void ProcessPlayerDeath()
    {
        if (PlayerLives > 0)
        {
            PlayerLives--;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            PlayerLives = 3;
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}