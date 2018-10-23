using UnityEngine;

public class BoardView : BaseView
{
    [SerializeField]
    private GameObject _FieldPrefab;

    protected override void DoAwake()
    {
        if (_FieldPrefab == null)
        {
            Debug.LogErrorFormat("Field prefab is not set in object: {0}", gameObject.name);
            return;
        }

        Entity.AddBoard(5,5);
        CreateFields();
    }
    private void CreateFields()
    {
        int rows = Entity.board.Rows;
        int columns = Entity.board.Columns;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Instantiate(_FieldPrefab, transform);
            }
        }
    }
}
