using Microsoft.Xna.Framework.Graphics;

public abstract class DrawableTile : Tile {
    private readonly Tile tile;
    public Tile Tile {
        get { return tile; }
    }
    public DrawableTile(Tile tile) {
        this.tile = tile;
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
    public abstract Texture2D GetImage();
}