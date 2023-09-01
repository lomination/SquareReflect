using System.Linq;
using Microsoft.Xna.Framework.Input;

public class GraphicController : IController<DrawableTile> {
    public (int playerId, Status newDirection)? GetInput() {
        Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
        if (keysPressed.Any(k => k == Keys.Up)) { return (0, Status.IsGoingUp); }
        else if (keysPressed.Any(k => k == Keys.Right)) { return (0, Status.IsGoingRight); }
        else if (keysPressed.Any(k => k == Keys.Down)) { return (0, Status.IsGoingDown); }
        else if (keysPressed.Any(k => k == Keys.Left)) { return (0, Status.IsGoingLeft); }
        else { return null; }
    }
    public void Display(SRGame<DrawableTile> game) {}
}
