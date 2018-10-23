using UnityEngine;

[RequireComponent((typeof(SpriteRenderer)))]
public class FieldView : BaseView, IPositionListener
{
    [SerializeField]
    private FieldConfiguration _Configuration;
    private SpriteRenderer _SpriteRenderer;

    protected override void DoAwake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        Entity.AddField(GetRandomType());
        Entity.AddPositionListener(this);
        Entity.AddPosition(Vector3.zero);
        SetSpriteByType();
    }

    public void OnPosition(GameEntity entity, Vector3 Value)
    {
        transform.position = Value;
    }

    private void OnMouseDown()
    {
        _SpriteRenderer.enabled = false;
    }

    private eFieldType GetRandomType()
    {
        return (eFieldType) Random.Range(0, 3);
    }

    private void SetSpriteByType()
    {
        switch (Entity.field.Value)
        {
            case eFieldType.Circle:
                _SpriteRenderer.sprite = _Configuration.CircleSprite;
                break;

            case eFieldType.Square:
                _SpriteRenderer.sprite = _Configuration.SquareSprite;
                break;

            case eFieldType.Triangle:
                _SpriteRenderer.sprite = _Configuration.TriangleSprite;
                break;
            default:
                Debug.LogErrorFormat("Value not handled {0}", Entity.field.Value);
                break;
        }
    }
}
