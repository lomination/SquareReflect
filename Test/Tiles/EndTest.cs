namespace Test;

[TestClass]
public class EndTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,4"
        );
        Game game = new(board, player);
        game.Play(true);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.HasFinished, game.Players[0].Status);
    }
}
