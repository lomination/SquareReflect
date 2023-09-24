public class DrawableTileConverter {
    private readonly ThemeManager themeManager;
    public DrawableTileConverter(ThemeManager themeManager) {
        this.themeManager = themeManager;
    }
    public DrawableTile ConvertTile(Tile tile) {
        return tile.GetId() switch {
            0 => new GenericDrawableTile(tile.Clone(), themeManager, 0),
            1 => new GenericDrawableTile(tile.Clone(), themeManager, 1),
            2 => new GenericDrawableTile(tile.Clone(), themeManager, 2, (int)((Angle)tile).Dir),
            3 => new GenericDrawableTile(tile.Clone(), themeManager, 3),
            4 => new GenericDrawableTile(tile.Clone(), themeManager, 4),
            5 => new GenericDrawableTile(tile.Clone(), themeManager, 5),
            6 => new GenericDrawableTile(tile.Clone(), themeManager, 6),
            // 7 => new GenericDrawableTile(tile.Clone(), themeManager, 7, ((Portal)tile).TileId),
            8 => new GenericDrawableTile(tile.Clone(), themeManager, 8, (int)((Blocker)tile).Dir),
            9 => new GenericDrawableTile(tile.Clone(), themeManager, 9, (int)((Arrow)tile).Dir),
            10 => new GenericDrawableTile(tile.Clone(), themeManager, 10, (int)((Tunnel)tile).Dir),
            11 => new GenericDrawableTile(tile.Clone(), themeManager, t => ((FragileBlock)t).Count > 0, 11, 0),
            12 => new GenericDrawableTile(tile.Clone(), themeManager, t => ((GhostBlock)t).Count > 0, 12, 1),
            13 => new GenericDrawableTile(tile.Clone(), themeManager, (int)((FragileAngle)tile).Dir, t => ((FragileAngle)t).Count > 0, 13, 0),
            14 => new GenericDrawableTile(tile.Clone(), themeManager, (int)((GhostAngle)tile).Dir, t => ((GhostAngle)t).Count > 0, 14, 2),
            15 => new GenericDrawableTile(tile.Clone(), themeManager, t => ((Key)t).IsTaken, 0, 15),
            16 => new GenericDrawableTile(tile.Clone(), themeManager, t => ((LockBlock)t).IsLocked, 16, 0),
            _ => new GenericDrawableTile(tile.Clone(), themeManager, 63)
        };
    }
    public Board<DrawableTile> ConvertBoard(Board<Tile> board) {
        return board.Convert(
            ConvertTile(board.DefaultTile),
            ConvertTile(board.BoarderTile),
            board.Grid.Select(l => l.Select(t => ConvertTile(t)).ToArray()).ToArray()
        );
    }
    public static Board<Tile> RestoreBoard(Board<DrawableTile> board) {
        return board.Convert(
            board.DefaultTile.Restore(),
            board.BoarderTile.Restore(),
            board.Grid.Select(l => l.Select(t => t.Restore()).ToArray()).ToArray()
        );
    }
}