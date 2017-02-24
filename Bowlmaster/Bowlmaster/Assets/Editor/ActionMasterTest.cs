using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class ActionMasterTest
{

    //private ActionMaster.Action _endTurn = ActionMaster.Action.EndTurn;
    //private ActionMaster.Action _tidy = ActionMaster.Action.Tidy;
    //private ActionMaster.Action _endGame = ActionMaster.Action.EndGame;
    //private ActionMaster.Action _reset = ActionMaster.Action.Reset;

    //[Test]
    //public void T01OneStrikeReturnsEndTurn()
    //{
    //    int[] rolls = {10};
    //    Assert.AreEqual(_endTurn, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T02Bowl8ReturnsTidy()
    //{
    //    int[] rolls = { 8 };
    //    Assert.AreEqual(_tidy, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T03After20BowlsReturnsEndGame()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    //    Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T04Bowl2And8SpareReturnsEndTurn()
    //{
    //    int[] rolls = {2, 8};
    //    Assert.AreEqual(_endTurn, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T05AtStrikeInLastFrameReturnsReset()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10};
    //    Assert.AreEqual(_reset, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T06AtSpareInLastFrameReturnsReset()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9};
    //    Assert.AreEqual(_reset, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T07YouTubeRollsReturnsEndGame()
    //{
    //    int[] rolls = {8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9};
    //    Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T08NotKnockAllPinsAfterStrikeOnBowl19ReturnsTidy()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 9};
    //    Assert.AreEqual(_tidy, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T09StrikeIn19AndZeroIn20ReturnsTidy()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0};
    //    Assert.AreEqual(_tidy, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T10NathanBowlIndexTest()
    //{
    //    // Bowl 0 pins and after that 10 pins.
    //    // That should not increment bowlNumber twice
    //    int[] rolls = { 0, 10, 5, 1 };
    //    Assert.AreEqual(_endTurn, ActionMaster.NextAction(rolls.ToList()));
    //}

    //[Test]
    //public void T11Dondi10thFrameTurkey()
    //{
    //    int[] rolls = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10};
    //    Assert.AreEqual(_endGame, ActionMaster.NextAction(rolls.ToList()));
    //}
}
