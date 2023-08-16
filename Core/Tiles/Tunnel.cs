public class Tunnel : Tile {
    private readonly Status dir;
    public Status Dir {get => dir;}
    public Tunnel(Status dir) {
        if (StatusClass.IsADir(dir)) {
            this.dir = dir;
        } else {
            throw new ArgumentException($"Invalid parameter \"dir\" while using \"Tunnel\" tile main constructor. Given : {dir}, expected : GoingUp, GoingRight, GoingDOwn, or GoingLeft");
        }
    }
    public Tunnel(string maybeDir) {
        if (int.TryParse(maybeDir, out int dir)) {
            if (dir >= 0 && dir <= 4) {
                this.dir = (Status)dir;
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Tunnel\" tile string constructor. Given : {dir}, expected : 0, 1, 2 or 3");
            }
        } else {
            throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Tunnel\" tile string constructor. Given : {maybeDir}, expected : 0, 1, 2 or 3");
        }
    }
    public override int GetId() {
        return 10;
    }
    public override Tunnel Clone() {
        return new Tunnel(dir);
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return dir == ((Angle)obj).Dir;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + dir.GetHashCode();
        return hash;
    }
    public override Player WhenApproching(Player player) {
        if (player.Status == StatusClass.GetNextDir(dir) || player.Status == StatusClass.GetPreviousDir(dir)) {
            return new Player(player, newStatus : Status.IsStopped);
        } else {
            return base.WhenApproching(player);
        }
    }
}