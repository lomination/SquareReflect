using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class StaticDrawableTile: DrawableTile {
    private Texture2D texture;
    public StaticDrawableTile(Tile tile, Texture2D texture) : base(tile) {
        this.texture = texture;
    }
    public override StaticDrawableTile Clone() {
        return new StaticDrawableTile(Tile.Clone(), texture);
    }
    public override Texture2D DisplayMe() {
        return texture;
    }
}