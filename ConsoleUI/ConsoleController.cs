public class ConsoleController : IController<ConsoleTile> {
    private Dictionary<ConsoleKey, (int playerId, Status playerNewDir)> controls = new() {
        {ConsoleKey.UpArrow, (0, Status.IsGoingUp)},
        {ConsoleKey.RightArrow, (0, Status.IsGoingRight)},
        {ConsoleKey.DownArrow, (0, Status.IsGoingDown)},
        {ConsoleKey.LeftArrow, (0, Status.IsGoingLeft)},
    };
    public Dictionary<ConsoleKey, (int playerId, Status playerNewDir)> Controls {
        get { return new(controls); }
        set { controls = value; }
    }
    public (int playerId, Status newDirection)? GetInput() {
        if (Console.KeyAvailable) {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (controls.ContainsKey(key)) {
                return controls[key];
            }
        }
        return null;
    }
    public void Display(SRGame<ConsoleTile> game) {
        string gameString = "";
        for (int y = 0; y < game.Board.GetYSize(); y++) {
            for (int x = 0; x < game.Board.GetXSize(); x++) {
                Position pos = new(x, y);
                if (game.Players.Any(p => p.Pos.Equals(pos))) {
                    gameString += '•';
                } else {
                    gameString += game[pos].DisplayMe();
                }
            }
            gameString += "\n";
        }
        Console.Clear();
        Console.WriteLine(gameString);
        Thread.Sleep(100);
    }
}
