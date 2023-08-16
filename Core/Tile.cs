public abstract class Tile {
    public abstract int GetId();
    public abstract Tile Clone();
    public override abstract bool Equals(object? obj);
    public override abstract int GetHashCode();
    public virtual Player WhenApproching(Player player) {
        return player;
    }
    public virtual Player WhenColliding(Player player) {
        return player.Continue();
    }
    public virtual Status[] GetStartDirs() {
        return new Status[] {};
    }
}
