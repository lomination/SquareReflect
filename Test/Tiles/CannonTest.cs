namespace Test;

[TestClass]
public class CannonTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = BoardBuilder.Create(
            "0,0,6,0,0"
        );
        Game game = new(board, player, new TestController());
        game.Play(2);
        game.MovePlayer(0, Status.IsGoingRight);
        game.Play(2);
        Assert.AreEqual(new Position(4, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
