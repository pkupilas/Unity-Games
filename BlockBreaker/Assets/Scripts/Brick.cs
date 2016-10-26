using UnityEngine;
using System.Collections;
using System.Linq;

public class Brick : MonoBehaviour
{
    public AudioClip crack;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public GameObject smoke;

    private bool isBreakable;
    private int timesHits;
    private LevelManager lvlManager;

    // Use this for initialization
    void Start ()
	{
        isBreakable = this.CompareTag("Breakable");
	    if (isBreakable)
	    {
	        breakableCount++;
        }
	    lvlManager = FindObjectOfType<LevelManager>();
	    timesHits = 0;
	}
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        int maxHits = hitSprites.Length;
        if (timesHits >= maxHits)
        {
            PuffSmoke();
            Destroy(gameObject);
            breakableCount--;
            lvlManager.CheckIfAllBricksDestroyed();
        }
        else
        {
            ChangeSprite();
        }
        timesHits++;
    }

    private void PuffSmoke()
    {
        GameObject brickSmoke = Instantiate(smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
        if (brickSmoke != null)
        {
            brickSmoke.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            Debug.LogError("Bricksomke isn't instantiated.");
        }
    }

    private void ChangeSprite()
    {
        if (hitSprites[timesHits]!=null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[timesHits];
        }
        else
        {
            Debug.LogError("Brick sprite is missing");
        }
    }

}
