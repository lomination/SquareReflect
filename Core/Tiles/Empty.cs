public class Empty : Tile {
    public Empty() {}
    public override int GetId() {
        return 0;
    }
    public override string Encode() {
        return $"{GetId()}";
    }
    public override Empty Clone() {
        return new Empty();
    }
    public override string ToString() {
        return " ";
    }
    public override bool Equals(object? obj) {
        return !(obj is null || GetType() != obj.GetType());
    }
    public override int GetHashCode() {
        return 17 + GetId();
    }
}
