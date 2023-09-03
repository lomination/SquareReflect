using Microsoft.Xna.Framework.Input;

public class GraphicController : IController<DrawableTile> {
    public (int playerId, Status newDirection)? GetInput() {
        return null;
    }
    public void Display(SRGame<DrawableTile> game) {}
}
