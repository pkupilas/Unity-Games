using UnityEngine;
using UnityEngine.UI;
using WorldObjects.Spawner;

public class WaveText : MonoBehaviour
{
    private Text _text;
    private EnemySpawner _enemySpawner;

    void Start()
    {
        _text = GetComponent<Text>();
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void LateUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"Wave: {_enemySpawner.CurrentWave}";
    }
}
