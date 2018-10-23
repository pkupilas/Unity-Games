using Entitas;
using Entitas.CodeGeneration.Attributes;
using Vector3 = UnityEngine.Vector3;

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
    public Vector3 Value;
}
