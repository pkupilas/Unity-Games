using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitView : MonoBehaviour
{
    [SerializeField]
    private string _NextLevelSceneName = string.Empty;

    [SerializeField]
    private float _LoadDelay = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag((Utilities.Constans.Tag.PLAYER)))
        {
            StartCoroutine(LoadNextLevelWithDelay());
        }
    }

    private IEnumerator LoadNextLevelWithDelay()
    {
        yield return new WaitForSeconds(_LoadDelay);

        SceneManager.LoadSceneAsync(_NextLevelSceneName);
    } 
}
