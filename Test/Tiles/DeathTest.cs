namespace Test;

[TestClass]
public class DeathTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,5"
        );
        Game game = new(board, player);
        game.Play(true);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsDead, game.Players[0].Status);
    }
}
