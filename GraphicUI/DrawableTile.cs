using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class DrawableTile : Tile {
    protected readonly Tile tile;
    public Tile Tile {
        get { return tile; }
    }
    protected readonly ThemeManager themeManager;
    public ThemeManager ThemeManager {
        get { return themeManager; }
    }
    public DrawableTile(Tile tile, ThemeManager themeManager) {
        this.tile = tile;
        this.themeManager = themeManager;
    }
    public override int GetId() {
        return tile.GetId();
    }
    public override abstract DrawableTile Clone();
    public override bool Equals(object? obj) {
        return tile.Equals(obj);
    }
    public override int GetHashCode() {
        return tile.GetHashCode();
    }
    public override Status[] GetStartDirs() {
        return tile.GetStartDirs();
    }
    public override Player WhenApproching(Player player) {
        return tile.WhenApproching(player);
    }
    public override Player WhenColliding(Player player) {
        return tile.WhenColliding(player);
    }
    public abstract void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle);
    public Tile Restore() {
        return tile;
    }
}
