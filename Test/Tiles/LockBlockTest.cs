namespace Test;

[TestClass]
public class LockBlockTest {
    [TestMethod]
    public void BehaviourWithNoKey() {
        Player player = new(new Position(0, 0), Status.IsGoingRight);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,16,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(1, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsStopped, game.Players[0].Status);
    }
    [TestMethod]
    public void BehaviourWithKey() {
        Player player = new Player(new Position(0, 0), Status.IsGoingRight).SetProp(PlayerProp.numberOfKeys, 1);
        Board<Tile> board = BoardBuilder.Create(
            "0,0,16,0"
        );
        SRGame<Tile> game = new(board, player, new TestController());
        game.Play(3);
        Assert.AreEqual(new Position(3, 0), game.Players[0].Pos);
        Assert.AreEqual(Status.IsGoingRight, game.Players[0].Status);
    }
}
