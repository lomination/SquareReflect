public class Game {
    private readonly Board board;
    public Board Board {get => board.Clone();}
    private readonly Player[] players;
    public Player[] Players {get => (from player in players select player).ToArray();}
    public Game(Board board) {
        this.board = board;
        Console.WriteLine(board);
        List<Player> players = new();
        for (int y = 0; y < board.GetYSize(); y++) {
            for (int x = 0; x < board.GetXSize(); x++) {
                if (board[x, y] is Start start) {
                    foreach (Status dir in start.Dirs) {
                        players.Add(new Player(new Position(x, y), dir));
                    }
                }
            }
        }
        if (players.Count() > 0 && players.Count() <= 4) {
            this.players = players.ToArray();
        } else {
            throw new ArgumentException($"\"board\" parameter contains a non valid number of player starts : {players.Count()}, expected from 1 to 4");
        }
    }
    public Game(Board board, Player[] players) {
        this.board = board;
        this.players = players;
    }
    public Game(Board board, Player player): this(board, new Player[] {player}) {}
    public Game(Board board, Player player1, Player player2): this(board, new Player[] {player1, player2}) {}
    public Game(Board board, Player player1, Player player2, Player player3): this(board, new Player[] {player1, player2,player3}) {}
    public Game(Board board, Player player1, Player player2, Player player3, Player player4): this(board, new Player[] {player1, player2,player3, player4}) {}

    public override string ToString() {
        string boardString = board.ToString();
        string gameString = "";
        for (int i = 0; i < boardString.Count(); i++) {
            if (players.Any(p => p.Pos["y"] * (board.GetXSize() + 1) + p.Pos["x"] == i)) {
                gameString += '•'; // TODO distinction visuelle des players?
            } else {
                gameString += boardString[i];
            }
        }
        return gameString;
    }
    public void  Play(bool testConfig = false, int maxSteps = -1) {
        int remainingSteps = maxSteps;
        for (int i = 0; i < players.Length; i++) {
            if (StatusClass.IsADir(players[i].Status)) {
                players[i] = board[board[players[i].Pos].WhenColliding(players[i]).Pos].WhenApproching(players[i]);
            }
        }
        bool hasFinished = players.Any(p => p.Status != Status.HasFinished);
        bool somebodyIsDead = !players.Any(p => p.Status == Status.IsDead);

        while (hasFinished && somebodyIsDead && remainingSteps != 0 && (!players.All(p => !StatusClass.IsADir(p.Status)) || !testConfig)) {
            if (!testConfig) {
                if (ConsoleController.TryGetInput(out (int playerId, Status playerNewDir) move)) {
                    if (players[move.playerId].Status == Status.IsStopped) {
                        players[move.playerId] = new Player(players[move.playerId], newStatus : move.playerNewDir, newNumOfMoves : players[move.playerId].NumOfMoves);
                    }
                }
                Console.Clear();
                Console.WriteLine(this);
                Thread.Sleep(100);
            }
            for (int i = 0; i < players.Length; i++) {
                if (StatusClass.IsADir(players[i].Status)) {
                    players[i] = board[players[i].Pos].WhenColliding(players[i]);
                    players[i] = board[board[players[i].Pos].WhenColliding(players[i]).Pos].WhenApproching(players[i]);
                }
            }
            remainingSteps -= 1;
        }
    }
    public void MovePlayer(int playerId, Status dir) {
        if (players[playerId].Status == Status.IsStopped) {
            players[playerId] = new Player(players[playerId], newStatus : dir, newNumOfMoves : players[playerId].NumOfMoves);
        }
    }
}
