public class Arrow : Tile {
    private readonly Status dir;
    public Status Dir {get => dir;}
    public Arrow(Status dir) {
        if (StatusClass.IsADir(dir)) {
            this.dir = dir;
        } else {
            throw new ArgumentException($"Invalid \"dir\" while using \"Shifter\" tile main constructor. Given : {dir}, expected : GoingUp, GoingRight, GoingDOwn, or GoingLeft");
        }
    }
    public Arrow(string maybeDir) {
        if (int.TryParse(maybeDir, out int dir)) {
            if (dir >= 0 && dir <= 4) {
                this.dir = (Status)dir;
            } else {
                throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Shifter\" tile string constructor. Given : {dir}, expected : 0, 1, 2 or 3");
            }
        } else {
            throw new ArgumentException($"Invalid direction in parameter \"maybeDir\" while using \"Shifter\" tile string constructor. Given : {maybeDir}, expected : 0, 1, 2 or 3");
        }
    }
    public override int GetId() {
        return 9;
    }
    public override string Encode() {
        return $"{GetId()}({(int)dir})";
    }
    public override Arrow Clone() {
        return new Arrow(dir);
    }
    public override string ToString() {
        return dir switch {
            Status.IsGoingUp => "î",
            Status.IsGoingRight => "→",
            Status.IsGoingDown => "↓",
            Status.IsGoingLeft => "←",
            _ => throw new Exception($"\"Shifter\" private field \"dir\" does not contain a direction and cannot be shown. Current value : {this.dir}, expected : GoingUp, GoingRight, GoingDOwn, or GoingLeft"),
        };
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            return dir == ((Arrow)obj).Dir;
        }
    }
    public override int GetHashCode() {
        int hash = 17 + GetId();
        hash = hash * 23 + dir.GetHashCode();
        return hash;
    }
    public override Player WhenColliding(Player player) {
        return new Player(player, newStatus : dir);
    }
}