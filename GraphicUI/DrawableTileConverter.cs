using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class DrawableTileConverter {
    private readonly ContentManager content;
    private readonly string assetsDirectory = "TileAssets/";
    public DrawableTileConverter(ContentManager content) {
        this.content = content;
    }
    public Texture2D Load(string name) {
        return content.Load<Texture2D>(assetsDirectory + name);
    }
    public DrawableTile ConvertTile(Tile tile) {
        return tile.GetId() switch {
            0 => new StaticDrawableTile(tile.Clone(), Load("Empty")),
            1 => new StaticDrawableTile(tile.Clone(), Load("Block")),
            2 => new StaticDrawableTile(tile.Clone(), Load($"Angle{(int)((Angle)tile).Dir}")),
            3 => new StaticDrawableTile(tile.Clone(), Load("Empty")),
            4 => new StaticDrawableTile(tile.Clone(), Load("End")),
            5 => new StaticDrawableTile(tile.Clone(), Load("Death")),
            6 => new StaticDrawableTile(tile.Clone(), Load("Cannon")),
            // 7 => new StaticTexturedDT(tile.Clone(), Load("Empty")), ((Portal)tile).PortalId.ToString()}),
            8 => new StaticDrawableTile(tile.Clone(), Load($"Blocker{(int)((Blocker)tile).Dir}")),
            9 => new StaticDrawableTile(tile.Clone(), Load($"Arrow{(int)((Arrow)tile).Dir}")),
            10 => new StaticDrawableTile(tile.Clone(), Load($"Tunnel{(int)((Tunnel)tile).Dir}")),
            // 11 => new DrawableTile(tile.Clone(), Load()),
            // 12 => new DrawableTile(tile.Clone(), Load()),
            // 13 => new DrawableTile(tile.Clone(), Load()),
            // 14 => new DrawableTile(tile.Clone(), Load()),
            // 15 => new StaticTexturedDT(tile.Clone(), Load()),
            // 16 => new StaticTexturedDT(tile.Clone(), Load()),
            _ => new StaticDrawableTile(tile.Clone(), Load("UnknownTile"))
        };
    }
    public Board<DrawableTile> ConvertBoard(Board<Tile> board) {
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