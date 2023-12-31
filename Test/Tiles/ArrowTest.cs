namespace Test;

[TestClass]
public class ArrowTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,9(0),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingUp, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,9(1),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,9(2),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(1, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingDown, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,9(3),0;" +
            "0,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(0, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingLeft, game.Players[0].Status);
    }
}
