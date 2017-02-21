using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ActionMasterTest
{

    private ActionMaster.Action _endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action _tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action _endGame = ActionMaster.Action.EndGame;
    private ActionMaster _actionMaster;

    [SetUp]
    public void Setup()
    {
         _actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1,1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(_endTurn, _actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(_tidy, _actionMaster.Bowl(8));
    }

    [Test]
    public void T03After21BowlsReturnsEndGame()
    {
        for (int i = 0; i < 20; i++)
        {
            _actionMaster.Bowl(1);
        }

        Assert.AreEqual(_endGame, _actionMaster.Bowl(1));
    }

    [Test]
    public void T04Bowl2And8SpareReturnsEndTurn()
    {
        _actionMaster.Bowl(2);
        Assert.AreEqual(_endTurn,_actionMaster.Bowl(8));
    }

}
