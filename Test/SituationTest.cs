namespace Test;

[TestClass]
public class SituationTest {
    [TestMethod]
    public void DoubleBlock() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,0,1;" +
            "0,0,1,0"
        );
        Game game = new(board, player);
        game.Play(true);
        game.MovePlayer(0, Status.IsGoingDown);
        game.Play(true);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void AngleWithBlockBehind() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,2(2);" +
            "0,0,1"
        );
        Game game = new(board, player);
        game.Play(true, 4);
        Assert.AreEqual(new Position(2, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingDown, game.Players[0].Status);
    }
    [TestMethod]
    public void PortalWithBlockBehind() {
        Player player = new(new Position(0, 0), Status.IsGoingDown);
        Board board = Board.Parse(
            "   0,        0,     0;" +
            "11(A-1-1),11(A-0-1),0;" +
            "   0,        1,     0"
        );
        Game game = new(board, player);
        game.Play(true);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(true, 2);
        Assert.AreEqual(new Position(6, 2), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
    [TestMethod]
    public void AngleThatLeadsToBlock() {
        Player player = new(new Position(2, 3), Status.IsGoingUp);
        Board board = Board.Parse(
            "0,0,0,0;" +
            "0,0,2(1),1;" +
            "0,0,0,0;" +
            "0,0,0,0"
        );
        Game game = new(board, player);
        game.Play(true);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        game.MovePlayer(0, Status.IsGoingUp);
        game.Play(true);
        Assert.AreEqual(new Position(2, 1), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
}
