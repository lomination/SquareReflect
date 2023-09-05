public static class ConsoleTileConverter {
    public static ConsoleTile ConvertTile(Tile tile) {
        return tile.GetId() switch {
            0 => new ConsoleTile(tile.Clone(), t => " "),
            1 => new ConsoleTile(tile.Clone(), t => "■"),
            2 => new ConsoleTile(tile.Clone(), t => "◣◤◥◢"[(int)((Angle)tile).Dir].ToString()),
            3 => new ConsoleTile(tile.Clone(), t => " "),
            4 => new ConsoleTile(tile.Clone(), t => "⚑"),
            5=> new ConsoleTile(tile.Clone(), t => "⨯"),
            6 => new ConsoleTile(tile.Clone(), t => "⊕"),
            7 => new ConsoleTile(tile.Clone(), t => ((Portal)tile).PortalId.ToString()),
            8 => new ConsoleTile(tile.Clone(), t => "⍓⍄⍌⍃"[(int)((Blocker)tile).Dir].ToString()),
            9 => new ConsoleTile(tile.Clone(), t => "⯭⯮⯯⯬"[(int)((Arrow)tile).Dir].ToString()),
            10 => new ConsoleTile(tile.Clone(), t => "▥▤"[(int)((Tunnel)tile).Dir % 2].ToString()),
            11 => new ConsoleTile(tile.Clone(), t => ((FragileBlock)t).Count > 0 ? "⯀" : "⛶"),
            12 => new ConsoleTile(tile.Clone(), t => ((GhostBlock)t).Count > 0 ? "⬚" : "⯀"),
            13 => new ConsoleTile(tile.Clone(), t => ((FragileAngle)t).Count > 0 ? "◣◤◥◢"[(int)((FragileAngle)tile).Dir].ToString() : "◺◸◹◿"[(int)((FragileAngle)tile).Dir].ToString()),
            14 => new ConsoleTile(tile.Clone(), t => ((GhostAngle)t).Count > 0 ? "◺◸◹◿"[(int)((GhostAngle)tile).Dir].ToString() : "◣◤◥◢"[(int)((GhostAngle)tile).Dir].ToString()),
            15 => new ConsoleTile(tile.Clone(), t => ((Key)t).IsTaken ? " " : "⚿"),
            16 => new ConsoleTile(tile.Clone(), t => ((LockBlock)t).IsLocked ? "🄺" : " "),
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