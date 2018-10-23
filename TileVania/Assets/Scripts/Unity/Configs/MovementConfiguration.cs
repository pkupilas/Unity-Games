using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfiguration", menuName = "Configs/Movement")]
public class MovementConfiguration : ScriptableObject
{
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private float _JumpSpeed;

    public float MovementSpeed => _MovementSpeed;
    public float JumpSpeed => _JumpSpeed;
}
