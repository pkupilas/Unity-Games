using UnityEngine;

public class Brick : MonoBehaviour
{
    public static int BreakableCount;

    [SerializeField] private AudioClip _crack;
    [SerializeField] private Sprite[] _hitSprites;
    [SerializeField] private GameObject _smoke;
    
    private bool _isBreakable;
    private int _timesHits;
    private LevelManager _lvlManager;
    
    void Start ()
	{
        _isBreakable = CompareTag("Breakable");
	    if (_isBreakable)
	    {
	        BreakableCount++;
        }
	    _lvlManager = FindObjectOfType<LevelManager>();
	}
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        AudioSource.PlayClipAtPoint(_crack, transform.position);
        if (_isBreakable)
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        int maxHits = _hitSprites.Length;
        if (_timesHits >= maxHits)
        {
            PuffSmoke();
            Destroy(gameObject);
            BreakableCount--;
            _lvlManager.CheckIfAllBricksDestroyed();
        }
        else
        {
            ChangeSprite();
        }
        _timesHits++;
    }

    private void PuffSmoke()
    {
        var brickSmoke = Instantiate(_smoke, gameObject.transform.position, Quaternion.identity);
        if (brickSmoke != null)
        {
            var particleSettings = brickSmoke.GetComponent<ParticleSystem>().main;
            particleSettings.startColor = GetComponent<SpriteRenderer>().color;
        }
        else
        {
            Debug.LogError("Bricksomke isn't instantiated.");
        }
    }

    private void ChangeSprite()
    {
        if (_hitSprites[_timesHits]!=null)
        {
            GetComponent<SpriteRenderer>().sprite = _hitSprites[_timesHits];
        }
        else
        {
            Debug.LogError("Brick sprite is missing");
        }
    }
}
