public class GhostAngle : Tile {
    private readonly Status dir;
    public Status Dir {get => dir;}
    private int count;
    public int Count {get => count;}
    public GhostAngle(Status dir, int count) {
        if (StatusClass.IsADir(dir)) {
            this.dir = dir;
        } else {
            throw new ArgumentException($"Invalid parameter \"dir\" while using \"GhostAngle\" tile main constructor. Given : {dir}, expected : GoingUp, GoingRight, GoingDOwn, or GoingLeft");
        }
        if (count > 0) {
            this.count = count;
        } else {
            throw new ArgumentException($"Invalid parameter \"count\" while using \"GhostAngle\" tile main constructor. Given : {count}, expected : non null positive int");
        }
    }
    public GhostAngle(string maybePrameters) {
        string[] parameters = maybePrameters.Split('-');
        if (int.TryParse(parameters[0], out int dir)) {
            if (dir >= 0 && dir <= 4) {
                this.dir = (Status)dir;
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"GhostAngle\" tile string constructor. Given : {dir}, expected : string containing 0, 1, 2 or 3");
            }
        } else {
            throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"GhostAngle\" tile string constructor. Given : {parameters[0]}, expected : string containing 0, 1, 2 or 3");
        }
        if (int.TryParse(parameters[1], out int count)) {
            if (count > 0) {
                this.count = count;
            } else {
                throw new ArgumentException($"Invalid count in parameter \"maybeParameters\" while using \"FragileBlock\" tile string constructor. Given : {count}, expected : string containing a non null positive int");
            }
        } else {
            throw new ArgumentException($"Invalid count in parameter \"maybeParameters\" while using \"FragileBlock\" tile string constructor. Given : {parameters[1]}, expected : string containing a non null positive int");
        }
    }
    public override int GetId() {
        return 14;
    }
    public override GhostAngle Clone() {
        return new GhostAngle(dir, count);
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return dir == ((GhostAngle)obj).dir && count == ((GhostAngle)obj).count;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + dir.GetHashCode();
        hash = hash * 23 + count;
        return hash;
    }
    public override Player WhenApproching(Player player) {
        if (count <= 0 && (player.Status == dir || player.Status == StatusClass.GetNextDir(dir))) {
            count -= 1;
                return new Player(player, newStatus : Status.IsStopped);
        } else {
            return base.WhenApproching(player);
        }
    }
    public override Player WhenColliding(Player player) {
        if (count > 0) {
            return base.WhenColliding(player);
        } else {
            if (player.Status == StatusClass.GetOppositeDir(dir)) {
                count -= 1;
                return new Player(player, newStatus : StatusClass.GetPreviousDir(dir));
            } else if (player.Status == StatusClass.GetPreviousDir(dir)) {
                count -= 1;
                return new Player(player, newStatus : StatusClass.GetOppositeDir(dir));
            } else {
                return base.WhenColliding(player);
            }
        }
    }
}
