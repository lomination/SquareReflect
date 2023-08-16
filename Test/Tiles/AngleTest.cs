namespace Test;

[TestClass]
public class AngleTest {
    [TestMethod]
    public void BehaviourDirUp() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,2(0),0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(2, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirRight() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,2(1),0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirDown() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,2(2),0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourDirLeft() {
        Player player = new(new Position(1, 0), Status.IsGoingDown);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0;" +
            "0,0,0;" +
            "0,2(3),0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(0, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingLeft, game.Players[0].Status);
    }
}
