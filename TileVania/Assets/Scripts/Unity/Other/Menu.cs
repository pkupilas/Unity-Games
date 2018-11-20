using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string _NextLevelSceneName = string.Empty;

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(_NextLevelSceneName);
    }
}
