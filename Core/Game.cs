public class Game {
    private readonly Board board;
    public Board Board {get => board.Clone();}
    private readonly Player[] players;
    public Player[] Players {get => (from player in players select player).ToArray();}
    private readonly IController controller;
    public Game(Board board, Player[] players, IController controller) {
        this.board = board;
        this.players = players;
        this.controller = controller;
    }
    public Game(Board board, Player player, IController controller) : this(board, new Player[] {player}, controller) {}
    public Game(Board board, Player player1, Player player2, IController controller) : this(board, new Player[] {player1, player2}, controller) {}
    public Game(Board board, Player player1, Player player2, Player player3, IController controller) : this(board, new Player[] {player1, player2,player3}, controller) {}
    public Game(Board board, Player player1, Player player2, Player player3, Player player4, IController controller) : this(board, new Player[] {player1, player2,player3, player4}, controller) {}
    public Game(Board board, IController controller) {
        this.board = board;
        this.controller = controller;
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
    public void Play(int maxSteps = -1) {
        int remainingSteps = maxSteps;
        /* for (int i = 0; i < players.Length; i++) {
            if (StatusClass.IsADir(players[i].Status)) {
                players[i] = board[board[players[i].Pos].WhenColliding(players[i]).Pos].WhenApproching(players[i]);
            }
        } */
        while (!IsEnded() && remainingSteps != 0) {
            (int playerId, Status newDirection)? maybeMove = controller.GetInput();
            if (maybeMove is (int, Status) move) {
                MovePlayer(move.playerId, move.newDirection);
            }
            controller.Display(this);
            RunPlayers();
            remainingSteps -= 1;
        }
    }
    public void MovePlayer(int playerId, Status dir) {
        if (players[playerId].Status == Status.IsStopped) {
            players[playerId] = new Player(players[playerId], newStatus : dir, newNumOfMoves : players[playerId].NumOfMoves);
        }
    }
    public bool IsEnded() {
        bool allFinished = players.All(p => p.Status == Status.HasFinished);
        bool somebodyDied = players.Any(p => p.Status == Status.IsDead);
        return allFinished && somebodyDied;
    }
    public void RunPlayers() {
        for (int i = 0; i < players.Length; i++) {
            if (StatusClass.IsADir(players[i].Status)) {
                players[i] = board[board[players[i].Pos].WhenColliding(players[i]).Pos].WhenApproching(players[i]);
                players[i] = board[players[i].Pos].WhenColliding(players[i]);
            }
        }
    }
}
