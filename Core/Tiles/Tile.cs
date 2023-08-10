using System.Text.RegularExpressions;

public abstract class Tile {
    public Tile() {}
    public abstract int GetId();
    public abstract string Encode();
    public abstract Tile Clone();
    public override abstract string ToString();
    // public override abstract bool Equals(object? obj);
    // public override abstract int GetHashCode();
    public virtual Player WhenApproching(Player player) {
        return player;
    }
    public virtual Player WhenColliding(Player player) {
        return player.Continue();
    }
    public static Tile Parse(string encodedTile) {
        Regex tileRegex = new(@"^0*(\d+)(?:\((.+)\))?$", RegexOptions.Compiled);
        Match match = tileRegex.Match(encodedTile);
        return match.Groups[1].Value switch {
            "0" => new Empty(),
            "1" => new Block(),
            "2" => new Angle(match.Groups[2].Value),
            "3" => new Start(match.Groups[2].Value),
            "4" => new End(),
            "5" => new Death(),
            "6" => new Cannon(),
            "7" => new Portal(match.Groups[2].Value),
            "8" => new Blocker(match.Groups[2].Value),
            "9" => new Arrow(match.Groups[2].Value),
            "10" => new Tunnel(match.Groups[2].Value),
            "11" => new FragileBlock(match.Groups[2].Value),
            "12" => new GhostBlock(match.Groups[2].Value),
            "13" => new FragileAngle(match.Groups[2].Value),
            "14" => new GhostAngle(match.Groups[2].Value),
            "15" => new Key(),
            "16" => new LockBlock(),
            _ => throw new ArgumentException($"Invalid tile code \"{match.Groups[1].Value}\" (from tile \"{encodedTile}\")"),
        };
    }
}
