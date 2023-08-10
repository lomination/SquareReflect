namespace Test;

[TestClass]
public class BlockTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,0,1"
        );
        Game game = new(board, player);
        game.Play(true, 3);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
}
