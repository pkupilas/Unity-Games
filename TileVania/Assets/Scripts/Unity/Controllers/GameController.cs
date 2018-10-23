using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Systems _Systems;
    private Contexts _Contexts;

    private void Awake()
    {
        _Contexts = Contexts.sharedInstance;
        _Systems = CreateSystems(_Contexts);
    }

    private void Start()
    {
        _Systems.Initialize();
    }

    private void Update()
    {
        _Systems.Execute();
        _Systems.Cleanup();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Systems();
    }

    private void OnDestroy()
    {
        _Systems.TearDown();
    }
}