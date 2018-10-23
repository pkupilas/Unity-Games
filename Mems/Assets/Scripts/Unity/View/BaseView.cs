using Entitas.Unity;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    protected GameEntity Entity;

    protected virtual void DoAwake() { }
    protected virtual void DoStart() { }
    protected virtual void DoUpdate() { }

    private void Awake()
    {
        Entity = Contexts.sharedInstance.game.CreateEntity();
        gameObject.Link(Entity, Contexts.sharedInstance.game);

        DoAwake();
    }

    private void Start()
    {
        DoStart();
    }

    private void Update()
    {
        DoUpdate();
    }
}
