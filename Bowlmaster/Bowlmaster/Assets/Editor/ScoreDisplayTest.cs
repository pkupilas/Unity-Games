using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Linq;

public class ScoreDisplayTest {

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1,1);
    }

    [Test]
    public void T01Roll1ReturnsOne()
    {
        int[] rolls = {1};
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
