public static class ConsoleController {
    private static readonly Dictionary<ConsoleKey, (int playerId, Status playerNewDir)> controls = new() {
            {ConsoleKey.UpArrow, (0, Status.IsGoingUp)},
            {ConsoleKey.RightArrow, (0, Status.IsGoingRight)},
            {ConsoleKey.DownArrow, (0, Status.IsGoingDown)},
            {ConsoleKey.LeftArrow, (0, Status.IsGoingLeft)},
        };
    public static bool TryGetInput(out (int playerId, Status playerNewDir) playerMove) {
        playerMove = (0, Status.IsStopped);
        if (Console.KeyAvailable) {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (controls.ContainsKey(key)) {
                playerMove = controls[key];
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
}
