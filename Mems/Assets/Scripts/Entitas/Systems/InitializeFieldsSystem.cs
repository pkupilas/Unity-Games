using Entitas;
using Vector3 = UnityEngine.Vector3;

public class InitializeFieldsSystem : IInitializeSystem
{
    private Contexts _Contexts;
    private IGroup<GameEntity> _FieldEnties;

    public InitializeFieldsSystem(Contexts contexts)
    {
        _Contexts = contexts;
        _FieldEnties = _Contexts.game.GetGroup(
            GameMatcher.AllOf(
                GameMatcher.Field)
            );
    }

    public void Initialize()
    {
        GameEntity[] entites = _FieldEnties.GetEntities();
        int rows = _Contexts.game.board.Rows;
        int columns = _Contexts.game.board.Columns;
        int k = 0;
        var offset = new Vector3(0.0f, 0.0f, 0.0f);
        float multiplier = 1.0f;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var newPosition = new Vector3(i * multiplier, j * multiplier, 0.0f);
                entites[k].ReplacePosition(newPosition + offset);
                k++;
            }
        }
    }
}
