public class TestController : IController<Tile> {
    public (int playerId, Status newDirection)? GetInput() {
        return null;
    }
    public void Display(SRGame<Tile> game) {}
}
