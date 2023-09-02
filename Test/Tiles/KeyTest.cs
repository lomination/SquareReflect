namespace Test;

[TestClass]
public class KeyTest {
    [TestMethod]
    public void Behaviour() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,15,0,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(3, 0), game.Players[0].Pos);
        Assert.AreEqual(1, game.Players[0].GetProp(PlayerProp.numberOfKeys));
        Assert.IsTrue(((Key)game[1, 0]).IsTaken);
    }
}
