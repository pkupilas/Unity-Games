using UnityEngine;

[CreateAssetMenu(fileName = "FieldConfiguration", menuName = "Configuration/Field")]
public class FieldConfiguration : ScriptableObject
{
    public Sprite CircleSprite => _CircleSprite;
    public Sprite SquareSprite => _SquareSprite;
    public Sprite TriangleSprite => _TriangleSprite;

    [SerializeField]
    private Sprite _CircleSprite;
    [SerializeField]
    private Sprite _SquareSprite;
    [SerializeField]
    private Sprite _TriangleSprite;
}
