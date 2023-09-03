public class Position {
    private readonly int[] coord;
    public int[] Coord {get => coord;}
    public Position(int[] coord) {
        this.coord = coord;
    }
    public Position(int x, int y) {
        coord = new int[] {x, y};
    }
    public override string ToString() {
        return string.Join(",", coord);
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            Position other = (Position)obj;
            return this[0] == other[0] && this[1] == other[1];
        }
    }
    public override int GetHashCode() {
        return ((System.Collections.IStructuralEquatable)coord).GetHashCode(EqualityComparer<int>.Default);
    }
    public int this[int index] {
        get => coord[index];
    }
    public int this[string str] {
        get => coord[new Dictionary<string, int>() {{"x", 0}, {"y", 1}}[str]];
    }
    public int this[char c] {
        get => coord[new Dictionary<char, int>() {{'x', 0}, {'y', 1}}[c]];
    }
    public Position Move(int deltaX, int deltaY) {
        return new Position(this["x"] + deltaX, this["y"] + deltaY);
    }
    public Position Move(Status dir) {
        if (dir == Status.IsGoingUp) {
            return Move(0, -1);
        } else if (dir == Status.IsGoingRight) {
            return Move(1, 0);
        } else if (dir == Status.IsGoingDown) {
            return Move(0, 1);
        } else if (dir == Status.IsGoingLeft) {
            return Move(-1, 0);
        } else {
            return this;
        }
    }
}
