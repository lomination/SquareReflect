namespace Test;

[TestClass]
public class FragileBlockTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,11(1),0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(1);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
