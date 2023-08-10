namespace Test;

[TestClass]
public class StatusClassTest {
    [TestMethod]
    public void IsADirectionTest() {
        Assert.IsTrue(StatusClass.IsADir(Status.IsGoingUp));
        Assert.IsTrue(StatusClass.IsADir(Status.IsGoingRight));
        Assert.IsTrue(StatusClass.IsADir(Status.IsGoingDown));
        Assert.IsTrue(StatusClass.IsADir(Status.IsGoingLeft));
        Assert.IsFalse(StatusClass.IsADir(Status.IsStopped));
        Assert.IsFalse(StatusClass.IsADir(Status.IsDead));
        Assert.IsFalse(StatusClass.IsADir(Status.HasFinished));
    }
    [TestMethod]
    public void GetNextDirTest() {
        Assert.AreEqual(Status.IsGoingRight, StatusClass.GetNextDir(Status.IsGoingUp));
        Assert.AreEqual(Status.IsGoingDown, StatusClass.GetNextDir(Status.IsGoingRight));
        Assert.AreEqual(Status.IsGoingLeft, StatusClass.GetNextDir(Status.IsGoingDown));
        Assert.AreEqual(Status.IsGoingUp, StatusClass.GetNextDir(Status.IsGoingLeft));
    }
    [TestMethod]
    public void GetOppositeDirTest() {
        Assert.AreEqual(Status.IsGoingDown, StatusClass.GetOppositeDir(Status.IsGoingUp));
        Assert.AreEqual(Status.IsGoingLeft, StatusClass.GetOppositeDir(Status.IsGoingRight));
        Assert.AreEqual(Status.IsGoingUp, StatusClass.GetOppositeDir(Status.IsGoingDown));
        Assert.AreEqual(Status.IsGoingRight, StatusClass.GetOppositeDir(Status.IsGoingLeft));
    }
    [TestMethod]
    public void GetPreviousDirTest() {
        Assert.AreEqual(Status.IsGoingLeft, StatusClass.GetPreviousDir(Status.IsGoingUp));
        Assert.AreEqual(Status.IsGoingUp, StatusClass.GetPreviousDir(Status.IsGoingRight));
        Assert.AreEqual(Status.IsGoingRight, StatusClass.GetPreviousDir(Status.IsGoingDown));
        Assert.AreEqual(Status.IsGoingDown, StatusClass.GetPreviousDir(Status.IsGoingLeft));
    }

}
