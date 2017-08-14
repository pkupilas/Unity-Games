using UnityEngine;
using UnityEngine.SceneManagement;

namespace CameraUI.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        public void LoadLevel(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void QuitRequest()
        {
            Application.Quit();
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}