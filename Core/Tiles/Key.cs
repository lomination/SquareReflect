public class Key : Tile {
    public Key() {}
    public override int GetId() {
        return 15;
    }
    public override string Encode() {
        return $"{GetId()}";
    }
    public override Key Clone() {
        return new Key();
    }
    public override string ToString() {
        return "K";
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        return 17 + GetId();
    }
    public override Player WhenColliding(Player player) {
        return player.SetProp(PlayerProp.numberOfKeys, player.GetProp(PlayerProp.numberOfKeys) + 1);
    }
}
