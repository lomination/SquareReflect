namespace Test;

[TestClass]
public class SituationTest {
    [TestMethod]
    public void DoubleBlock() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,0,1;" +
            "0,0,1,0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(1);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void AngleWithBlockBehind() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,2(2);" +
            "0,0,1"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void PortalWithBlockBehind() {
        Player player = new(new Position(0, 0), Status.IsGoingDown);
        Board<GenericTile> board = BoardBuilder.Create(
            "   0,      0,     0;" +
            "7(A-1-1),7(A-0-1),0;" +
            "   0,      1,     0"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(1);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void AngleThatLeadsToBlock() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<GenericTile> board = BoardBuilder.Create(
            "0,0,2(2);" +
            "0,0,1"
        );
        SRGame<GenericTile> game = new(board, player, new TestController());
        game.Play(3);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(2);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
}
