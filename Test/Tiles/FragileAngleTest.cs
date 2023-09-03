namespace Test;

[TestClass]
public class FragileAngleTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,13(0-1),0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(2, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
        Assert.AreEqual(0, ((FragileAngle)game[1, 2]).Count);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,13(1-1),0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((FragileAngle)game[1, 2]).Count);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,13(2-1),0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        Assert.AreEqual(0, ((FragileAngle)game[1, 2]).Count);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,13(3-1),0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(0, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingLeft, game.Players[0].Status);
        Assert.AreEqual(0, ((FragileAngle)game[1, 2]).Count);
    }
}
