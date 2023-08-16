public class End : GenericTile {
    public End() {}
    public override int GetId() {
        return 4;
    }
    public override End Clone() {
        return new End();
    }
    public override string ToString() {
        return "⚑";
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        return 17 + GetId();
    }
    public override Player WhenColliding(Player player) {
        return new Player(player, newStatus : Status.HasFinished);
    }
}
