using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfiguration", menuName = "Configs/Movement")]
public class MovementConfiguration : ScriptableObject
{
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private float _ClimbSpeed;
    [SerializeField] private float _JumpSpeed;

    public float MovementSpeed => _MovementSpeed;
    public float ClimbSpeed => _ClimbSpeed;
    public float JumpSpeed => _JumpSpeed;
}
