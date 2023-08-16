public class Block : Tile {
    public Block() {}
    public override int GetId() {
        return 1;
    }
    public override Block Clone() {
        return new Block();
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        return 17 + GetId();
    }
    public override Player WhenApproching(Player player) {
        return new Player(player, newStatus : Status.IsStopped);
    }
}
