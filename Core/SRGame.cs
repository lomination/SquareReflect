public class SRGame<T> where T : Tile {
    private readonly Board<T> board;
    public Board<T> Board {get => board.Clone();}
    private readonly Player[] players;
    public Player[] Players {get => (from player in players select player).ToArray();}
    private readonly IController<T> controller;
    public SRGame(Board<T> board, Player[] players, IController<T> controller) {
        this.board = board;
        this.players = players;
        this.controller = controller;
    }
    public SRGame(Board<T> board, Player player, IController<T> controller) : this(board, new Player[] {player}, controller) {}
    public SRGame(Board<T> board, Player player1, Player player2, IController<T> controller) : this(board, new Player[] {player1, player2}, controller) {}
    public SRGame(Board<T> board, Player player1, Player player2, Player player3, IController<T> controller) : this(board, new Player[] {player1, player2,player3}, controller) {}
    public SRGame(Board<T> board, Player player1, Player player2, Player player3, Player player4, IController<T> controller) : this(board, new Player[] {player1, player2,player3, player4}, controller) {}
    public SRGame(Board<T> board, IController<T> controller) {
        this.board = board;
        this.controller = controller;
        List<Player> players = new();
        for (int y = 0; y < board.GetYSize(); y++) {
            for (int x = 0; x < board.GetXSize(); x++) {
                foreach (Status dir in board[x, y].GetStartDirs()) {
                    players.Add(new Player(new Position(x, y), dir));
                }
            }
        }
        if (players.Count() > 0 && players.Count() <= 4) {
            this.players = players.ToArray();
        } else {
            throw new ArgumentException($"\"board\" parameter contains a non valid number of player starts : {players.Count()}, expected from 1 to 4");
        }
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            SRGame<Tile> other = (SRGame<Tile>)obj;
            if (board.Equals(other.board) && players.Length == other.players.Length) {
                for (int i = 0; i < players.Length; i ++) {
                    if (players[i].Equals(other.players[i])) {
                        return false;
                    }
                }
                return true;
            } else {
                return false;
            }
        }
    }
    public override int GetHashCode() {
        int hash = 17 + board.GetHashCode();
        foreach (Player player in players) {
            hash = hash * 23 + player.GetHashCode();
        }
        return hash;
    }
    public T this[int x, int y] {
        get => board[x, y];
    }
    public T this[Position pos] {
        get => board[pos];
    }
    public int GetXSize() {
        return board.GetXSize();
    }
    public int GetYSize() {
        return board.GetYSize();
    }
    public void Play(int maxSteps = -1) {
        int remainingSteps = maxSteps;
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
        if (playerId < players.Length && players[playerId].Status == Status.IsStopped) {
            players[playerId] = new Player(players[playerId], newStatus : dir, newNumOfMoves : players[playerId].NumOfMoves);
        }
    }
    public bool IsEnded() {
        bool allFinished = players.All(p => p.Status == Status.HasFinished);
        bool somebodyDied = players.Any(p => p.Status == Status.IsDead);
        return allFinished || somebodyDied;
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
