namespace Test;

[TestClass]
public class EndTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = BoardBuilder.Create(
            "0,0,4"
        );
        Game game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(2, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.HasFinished, game.Players[0].Status);
    }
}
