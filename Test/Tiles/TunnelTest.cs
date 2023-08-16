namespace Test;

[TestClass]
public class TunnelTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,10(0)"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,10(1)"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,10(2)"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,10(3)"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(2);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
