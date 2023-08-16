public class Key : Tile {
    private bool isTaken;
    public bool IsTaken {
        get => isTaken;
    }
    
    public Key() {
        isTaken = false;
    }
    public override int GetId() {
        return 15;
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
        if (!isTaken) {
            isTaken = true;
            return player.SetProp(PlayerProp.numberOfKeys, player.GetProp(PlayerProp.numberOfKeys) + 1).Continue();
        } else {
            return player.Continue();
        }
    }
}
