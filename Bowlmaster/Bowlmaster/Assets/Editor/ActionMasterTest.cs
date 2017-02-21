using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ActionMasterTest
{

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1,1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        var actionMaster = new ActionMaster();
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
}
