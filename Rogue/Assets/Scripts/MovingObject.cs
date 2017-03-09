using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{

    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private float inverseMoveTime;

	// Use this for initialization
	protected  virtual void Start ()
	{

	    _collider = GetComponent<BoxCollider2D>();
	    _rigidbody = GetComponent<Rigidbody2D>();
	    inverseMoveTime = 1f / moveTime;
	}

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(_rigidbody.position, end, inverseMoveTime * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            // wait for a frame before reevaluating the condition of the loop
            yield return null;
        }
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // do not hit own collider when casting ray
        _collider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        _collider.enabled = false;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

}
