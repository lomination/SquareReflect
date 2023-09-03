namespace Test;

[TestClass]
public class GhostBlockTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,12(1),0,0,1"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(6);
        Assert.AreEqual(new Position(4, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        game.MovePlayer(0, Status.IsGoingLeft);
        game.Play(3);
        Assert.AreEqual(new Position(3, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
}
