public class Angle : Tile {
    private readonly Status dir;
    public Status Dir {get => dir;}
    public Angle(Status dir) {
        if (StatusClass.IsADir(dir)) {
            this.dir = dir;
        } else {
            throw new ArgumentException($"Invalid parameter \"dir\" while using \"Angle\" tile main constructor. Given : {dir}, expected : GoingUp, GoingRight, GoingDOwn, or GoingLeft");
        }
    }
    public Angle(string maybeDir) {
        if (int.TryParse(maybeDir, out int dir)) {
            if (dir >= 0 && dir <= 4) {
                this.dir = (Status)dir;
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Angle\" tile string constructor. Given : {dir}, expected : 0, 1, 2 or 3");
            }
        } else {
            throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Angle\" tile string constructor. Given : {maybeDir}, expected : 0, 1, 2 or 3");
        }
    }
    public override int GetId() {
        return 2;
    }
    public override Angle Clone() {
        return new Angle(dir);
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return dir == ((Angle)obj).dir;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + dir.GetHashCode();
        return hash;
    }
    public override Player WhenApproching(Player player) {
        if (player.Status == dir || player.Status == StatusClass.GetNextDir(dir)) {
            return new Player(player, newStatus : Status.IsStopped);
        } else {
            return base.WhenApproching(player);
        }
    }
    public override Player WhenColliding(Player player) {
        if (player.Status == StatusClass.GetOppositeDir(dir)) {
            return new Player(player, newStatus : StatusClass.GetNextDir(dir)).Continue();
        } else if (player.Status == StatusClass.GetPreviousDir(dir)) {
            return new Player(player, newStatus : dir).Continue();
        } else {
            return base.WhenColliding(player);
        }
    }
}
