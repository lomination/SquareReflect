using Microsoft.Xna.Framework.Graphics;

public static class DrawableTileConverter {
    public static DrawableTile ConvertTile(Tile tile) {
        return tile.GetType().Name switch {
            nameof(Empty) => new DrawableTile(tile.Clone(), t => new string[] {"Empty"}),
            nameof(Block) => new DrawableTile(tile.Clone(), t => new string[] {"Block"}),
            nameof(Angle) => new DrawableTile(tile.Clone(), t => new string[] {$"Angle-{(int)((Angle)tile).Dir}"}),
            nameof(Start) => new DrawableTile(tile.Clone(), t => new string[] {"Empty"}),
            nameof(End) => new DrawableTile(tile.Clone(), t => new string[] {"End"}),
            nameof(Death) => new DrawableTile(tile.Clone(), t => new string[] {"Death"}),
            nameof(Cannon) => new DrawableTile(tile.Clone(), t => new string[] {"Cannon"}),
            nameof(Portal) => new DrawableTile(tile.Clone(), t => new string[] {"Portal", ((Portal)tile).PortalId.ToString()}),
            nameof(Blocker) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(Arrow) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(Tunnel) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(FragileBlock) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(GhostBlock) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(FragileAngle) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(GhostAngle) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(Key) => new DrawableTile(tile.Clone(), t => new string[] {}),
            nameof(LockBlock) => new DrawableTile(tile.Clone(), t => new string[] {}),
            _ => throw new Exception()
        };
    }
    public static Board<DrawableTile> ConvertBoard(Board<Tile> board) {
        return new Board<DrawableTile>(
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