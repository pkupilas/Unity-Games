using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class BoardComponent : IComponent
{
    public int Rows;
    public int Columns;
}
