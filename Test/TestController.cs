public class TestController : IController<GenericTile> {
    public (int playerId, Status newDirection)? GetInput() {
        return null;
    }
    public void Display(SRGame<GenericTile> game) {}
}
