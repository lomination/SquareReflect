namespace Test;

[TestClass]
public class PortalTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board board = BoardBuilder.Create(
            "0,0,7(A-4-0),5,7(A-2-0),0,0"
        );
        Game game = new(board, player, new TestController());
        game.Play(5);
        Assert.AreEqual(new Position(6, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
