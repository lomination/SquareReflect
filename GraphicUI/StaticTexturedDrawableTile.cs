using Microsoft.Xna.Framework.Graphics;

public class StaticTexturedDrawableTile : DrawableTile {
    private readonly Texture2D texture;
    public StaticTexturedDrawableTile(Tile tile, Texture2D texture) : base(tile) {
        this.texture = texture;
    }
    public override StaticTexturedDrawableTile Clone() {
        return new StaticTexturedDrawableTile(Tile.Clone(), texture);
    }
    public override Texture2D GetImage() {
        return texture;
    }
}