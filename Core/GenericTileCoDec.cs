using System.Text.RegularExpressions;

public class GenericTileCoDec : ITileCoDec<GenericTile> {
    public GenericTile Read(string encodedTile) {
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
            _ => throw new ArgumentException($"Failed to read IGenericTile in GenericTileCoDec: invalid IGenericTile code \"{match.Groups[1].Value}\" (from tile \"{encodedTile}\")"),
        };
    }
    public string Write(GenericTile genericTile) {
        return genericTile.GetType().Name switch {
            nameof(Empty) => "0",
            nameof(Block) => "1",
            nameof(Angle) => $"2({(int)((Angle)genericTile).Dir})",
            nameof(Start) => $"3({string.Join('-', from dir in ((Start)genericTile).Dirs select (int)dir)})",
            nameof(End) => "4",
            nameof(Death) => "5",
            nameof(Cannon) => "6",
            nameof(Portal) => $"7({((Portal)genericTile).PortalId}-{((Portal)genericTile).PairPos["x"]}-{((Portal)genericTile).PairPos["y"]})",
            nameof(Blocker) => $"8({(int)((Blocker)genericTile).Dir})",
            nameof(Arrow) => $"9({(int)((Arrow)genericTile).Dir})",
            nameof(Tunnel) => $"10({(int)((Tunnel)genericTile).Dir})",
            nameof(FragileBlock) => $"11({((FragileBlock)genericTile).Count})",
            nameof(GhostBlock) => $"12({((GhostBlock)genericTile).Count})",
            nameof(FragileAngle) => $"13({((FragileAngle)genericTile).Dir}-{((FragileAngle)genericTile).Count})",
            nameof(GhostAngle) => $"14({((GhostAngle)genericTile).Dir}-{((GhostAngle)genericTile).Count})",
            nameof(Key) => "15",
            nameof(LockBlock) => "16",
            _ => throw new ArgumentException($"Failed to write IGenericTile in GenericTileCoDec: invalid IGenericTile \"{genericTile.GetType()}\""),
        };
    }
}
