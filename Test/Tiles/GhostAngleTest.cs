namespace Test;

[TestClass]
public class GhostAngleTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Player player = new(new Position(1, 3), Status.IsGoingUp);
        Board<Tile> board = BoardBuilder.Create(
            "0,1,0;" +
            "0,0,0;" +
            "0,14(0-1),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((GhostAngle)game[1, 2]).Count);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(2);
        Assert.AreEqual(new Position(2, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Player player = new(new Position(1, 3), Status.IsGoingUp);
        Board<Tile> board = BoardBuilder.Create(
            "0,1,0;" +
            "0,0,0;" +
            "0,14(1-1),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((GhostAngle)game[1, 2]).Count);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(2);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Player player = new(new Position(1, 3), Status.IsGoingUp);
        Board<Tile> board = BoardBuilder.Create(
            "0,1,0;" +
            "0,0,0;" +
            "0,14(2-1),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((GhostAngle)game[1, 2]).Count);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(2);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Player player = new(new Position(1, 3), Status.IsGoingUp);
        Board<Tile> board = BoardBuilder.Create(
            "0,1,0;" +
            "0,0,0;" +
            "0,14(3-1),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((GhostAngle)game[1, 2]).Count);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(2);
        Assert.AreEqual(new Position(0, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingLeft, game.Players[0].Status);
    }
}
