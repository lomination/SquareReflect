public class GhostBlock : Tile {
    private int count;
    public int Count {get => count;}
    public GhostBlock(int count) {
        this.count = count;
    }
    public GhostBlock(string maybeCount) {
        if (int.TryParse(maybeCount, out int count)) {
            if (count > 0) {
                this.count = count;
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeCount\" while using \"GhostBlock\" tile string constructor. Given : {count}, expected : string containing a non null positive int");
            }
        } else {
            throw new ArgumentException($"Invalid direction in parameter \"maybeCount\" while using \"GhostBlock\" tile string constructor. Given : {maybeCount}, expected : string containing a non null positive int");
        }
    }
    public override int GetId() {
        return 12;
    }
    public override GhostBlock Clone() {
        return new GhostBlock(count);
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return count == ((GhostBlock)obj).count;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + count;
        return hash;
    }
    public override Player WhenApproching(Player player) {
        if (count > 0) {
            count -= 1;
            return base.WhenApproching(player);
        } else {
            return new Player(player, newStatus : Status.IsStopped);
        }
    }
}