using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ActionMasterTest
{

    private ActionMaster.Action _endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action _tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action _endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action _reset = ActionMaster.Action.Reset;
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
    public void T03After20BowlsReturnsEndGame()
    {
        for (int i = 0; i < 19; i++)
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

    [Test]
    public void T05AtStrikeInLastFrameReturnsReset()
    {
        int[] rolls = new int[18];

        for (int i = 0; i < rolls.Length; i++)
        {
            rolls[i] = 1;
            _actionMaster.Bowl(rolls[i]);
        }

        Assert.AreEqual(_reset,_actionMaster.Bowl(10));
    }

    [Test]
    public void T06AtSpareInLastFrameReturnsReset()
    {
        int[] rolls = new int[18];

        for (int i = 0; i < rolls.Length; i++)
        {
            rolls[i] = 1;
            _actionMaster.Bowl(rolls[i]);
        }
        _actionMaster.Bowl(1);
        Assert.AreEqual(_reset, _actionMaster.Bowl(9));
    }

    [Test]
    public void T07YouTubeRollsReturnsEndGame()
    {
        int[] rolls = {8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2};

        foreach (var roll in rolls)
        {
            _actionMaster.Bowl(roll);
        }

        Assert.AreEqual(_endGame,_actionMaster.Bowl(9));
    }

}
