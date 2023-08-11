public class ConsoleController : IController {
    private Dictionary<ConsoleKey, (int playerId, Status playerNewDir)> controls = new() {
        {ConsoleKey.UpArrow, (0, Status.IsGoingUp)},
        {ConsoleKey.RightArrow, (0, Status.IsGoingRight)},
        {ConsoleKey.DownArrow, (0, Status.IsGoingDown)},
        {ConsoleKey.LeftArrow, (0, Status.IsGoingLeft)},
    };
    public Dictionary<ConsoleKey, (int playerId, Status playerNewDir)> Controls {get => new(controls);}
    public (int playerId, Status newDirection)? GetInput() {
        if (Console.KeyAvailable) {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (controls.ContainsKey(key)) {
                return controls[key];
            }
        }
        return null;
    }
    public void Display(Game game) {
        Console.Clear();
        Console.WriteLine(game);
        Thread.Sleep(100);
    }
}
