using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class DrawableTileConverter {
    private ContentManager content;
    public DrawableTileConverter(ContentManager content) {
        this.content = content;
    }
    public Texture2D load(string name) {
        return this.content.Load<Texture2D>(name);
    }
    public DrawableTile ConvertTile(Tile tile) {
        return tile.GetType().Name switch {
            nameof(Empty) => new StaticDrawableTile(tile.Clone(), load("Empty")),
            nameof(Block) => new StaticDrawableTile(tile.Clone(), load("Block")),
            nameof(Angle) => new StaticDrawableTile(tile.Clone(), load($"Angle-{(int)((Angle)tile).Dir}")),
            nameof(Start) => new StaticDrawableTile(tile.Clone(), load("Empty")),
            // nameof(End) => new StaticDrawableTile(tile.Clone(), load("End")),
            // nameof(Death) => new StaticDrawableTile(tile.Clone(), load("Death")),
            // nameof(Cannon) => new StaticDrawableTile(tile.Clone(), load("Cannon")),
            nameof(Portal) => new StaticDrawableTile(tile.Clone(), load("Empty")), // ((Portal)tile).PortalId.ToString()}),
            // nameof(Blocker) => new StaticDrawableTile(tile.Clone(), load(}),
            // nameof(Arrow) => new DrawableTile(tile.Clone(), load(}),
            // nameof(Tunnel) => new DrawableTile(tile.Clone(), load(}),
            // nameof(FragileBlock) => new DrawableTile(tile.Clone(), load(}),
            // nameof(GhostBlock) => new DrawableTile(tile.Clone(), load(}),
            // nameof(FragileAngle) => new DrawableTile(tile.Clone(), load(}),
            // nameof(GhostAngle) => new DrawableTile(tile.Clone(), load(}),
            // nameof(Key) => new DrawableTile(tile.Clone(), load(}),
            // nameof(LockBlock) => new DrawableTile(tile.Clone(), load(}),
            _ => new StaticDrawableTile(tile.Clone(), load("Empty")) //throw new Exception()
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