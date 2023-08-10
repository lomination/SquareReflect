public class LockBlock : Tile {
    private bool isLocked;
    public bool IsLocked {get => isLocked;}
    public LockBlock() {
        isLocked = true;
    }
    public override int GetId() {
        return 16;
    }
    public override string Encode() {
        return $"{GetId()}";
    }
    public override LockBlock Clone() {
        return new LockBlock();
    }
    public override string ToString() {
        return "⯀";
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + isLocked.GetHashCode();
        return hash;
    }
    public override Player WhenApproching(Player player) {
        if (isLocked) {
            if (player.GetProp(PlayerProp.numberOfKeys) > 0) {
                isLocked = false;
                return player.SetProp(PlayerProp.numberOfKeys, player.GetProp(PlayerProp.numberOfKeys) - 1);
            } else {
                return new Player(player, newStatus : Status.IsStopped);
            }
        } else {
            return base.WhenApproching(player);
        }
    }
}
