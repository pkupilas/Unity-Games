using Entitas.Unity;
using UnityEngine;

public class EntityHostView : MonoBehaviour
{
    private void Awake()
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        gameObject.Link(gameEntity, Contexts.sharedInstance.game);
    }

    private void OnDestroy()
    {
        gameObject.Unlink();
    }
}
