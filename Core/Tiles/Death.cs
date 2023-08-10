public class Death : Tile {
    public Death() {}
    public override int GetId() {
        return 5;
    }
    public override string Encode() {
        return $"{GetId()}";
    }
    public override Death Clone() {
        return new Death();
    }
    
    public override string ToString() {
        return "+";
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        return 17 + GetId();
    }
    public override Player WhenColliding(Player player) {
        return new Player(player, newStatus : Status.IsDead);
    }
}
