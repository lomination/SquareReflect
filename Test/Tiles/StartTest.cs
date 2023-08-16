namespace Test;

[TestClass]
public class StartTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,3(0),0;" +
            "0,0,0"
        );
        Console.WriteLine(board);
        SRGame<GenericTile> game = new(board, new TestController());
        game.Play(1);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingUp, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,3(1),0;" +
            "0,0,0"
        );
        SRGame<GenericTile> game = new(board, new TestController());
        game.Play(1);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,3(2),0;" +
            "0,0,0"
        );
        SRGame<GenericTile> game = new(board, new TestController());
        game.Play(1);
        Assert.AreEqual(new Position(1, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingDown, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,3(3),0;" +
            "0,0,0"
        );
        SRGame<GenericTile> game = new(board, new TestController());
        game.Play(1);
        Assert.AreEqual(new Position(0, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingLeft, game.Players[0].Status);
    }
}
