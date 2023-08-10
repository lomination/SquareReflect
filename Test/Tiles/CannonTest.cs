namespace Test;

[TestClass]
public class CannonTest {
    [TestMethod]
    public void BehaviourWhenGoingUp() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = Board.Parse(
            "0,0,6,0,0"
        );
        Game game = new(board, player);
        game.Play(true);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(true, 2);
        Assert.AreEqual(new Position(5, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
