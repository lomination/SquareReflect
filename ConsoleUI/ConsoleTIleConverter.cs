public static class ConsoleTileConverter {
    public static ConsoleTile ConvertTile(Tile tile) {
        return tile.GetType().Name switch {
            nameof(Empty) => new ConsoleTile(tile.Clone(), t => " "),
            nameof(Block) => new ConsoleTile(tile.Clone(), t => "■"),
            nameof(Angle) => new ConsoleTile(tile.Clone(), t => "◣◤◥◢"[(int)((Angle)tile).Dir].ToString()),
            nameof(Start) => new ConsoleTile(tile.Clone(), t => " "),
            nameof(End) => new ConsoleTile(tile.Clone(), t => "⚑"),
            nameof(Death) => new ConsoleTile(tile.Clone(), t => "⨯"),
            nameof(Cannon) => new ConsoleTile(tile.Clone(), t => "⊕"),
            nameof(Portal) => new ConsoleTile(tile.Clone(), t => ((Portal)tile).PortalId.ToString()),
            nameof(Blocker) => new ConsoleTile(tile.Clone(), t => "⍓⍄⍌⍃"[(int)((Blocker)tile).Dir].ToString()),
            nameof(Arrow) => new ConsoleTile(tile.Clone(), t => "⯭⯮⯯⯬"[(int)((Arrow)tile).Dir].ToString()),
            nameof(Tunnel) => new ConsoleTile(tile.Clone(), t => "▥▤"[(int)((Tunnel)tile).Dir % 2].ToString()),
            nameof(FragileBlock) => new ConsoleTile(tile.Clone(), t => ((FragileBlock)t).Count > 0 ? "⯀" : "⛶"),
            nameof(GhostBlock) => new ConsoleTile(tile.Clone(), t => ((GhostBlock)t).Count > 0 ? "⬚" : "⯀"),
            nameof(FragileAngle) => new ConsoleTile(tile.Clone(), t => ((FragileAngle)t).Count > 0 ? "◣◤◥◢"[(int)((FragileAngle)tile).Dir].ToString() : "◺◸◹◿"[(int)((FragileAngle)tile).Dir].ToString()),
            nameof(GhostAngle) => new ConsoleTile(tile.Clone(), t => ((GhostAngle)t).Count > 0 ? "◺◸◹◿"[(int)((GhostAngle)tile).Dir].ToString() : "◣◤◥◢"[(int)((GhostAngle)tile).Dir].ToString()),
            nameof(Key) => new ConsoleTile(tile.Clone(), t => ((Key)t).IsTaken ? " " : "⚿"),
            nameof(LockBlock) => new ConsoleTile(tile.Clone(), t => ((LockBlock)t).IsLocked ? "🄺" : " "),
            _ => throw new Exception()
        };
    }
    public static Board<ConsoleTile> ConvertBoard(Board<Tile> board) {
        return new Board<ConsoleTile>(
            board.Title,
            board.Author,
            board.Date,
            board.Difficulty,
            board.Version,
            (from line in board.Grid select (from tile in line select ConvertTile(tile)).ToArray()).ToArray(),
            ConvertTile(board.Default)
        );
    }
}