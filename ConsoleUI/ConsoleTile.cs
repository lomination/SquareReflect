public class ConsoleTile : Tile {
    private Tile tile;
    public Tile Tile {
        get { return tile; }
        set { tile = value; }
    }
    private Func<Tile, string> display;
    public Func<Tile, string> Display {
        get { return display; }
        set { display = value; }
    }
    public ConsoleTile(Tile tile, Func<Tile, string> display) {
        this.tile = tile;
        this.display = display;
    }
    public override int GetId() {
        return tile.GetId();
    }
    public override ConsoleTile Clone() {
        return new ConsoleTile(tile.Clone(), display);
    }
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
    public string DisplayMe() {
        return display(tile);
    }
}