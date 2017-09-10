using UnityEngine;
using UnityEngine.UI;
using WorldObjects.Spawner;

public class EnemyCountImage : MonoBehaviour
{
    private Image _image;
    private EnemySpawner _enemySpawner;

    void Start()
    {
        _image = GetComponent<Image>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void LateUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _image.enabled = !_enemySpawner.IsBreak;
    }
}