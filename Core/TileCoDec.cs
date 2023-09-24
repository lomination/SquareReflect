using System.Text.RegularExpressions;

public static class TileCoDec {
    public static Tile Read(string encodedTile) {
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
            _ => throw new ArgumentException($"Failed to read Tile in TileCoDec: invalid Tile code \"{match.Groups[1].Value}\" (from tile \"{encodedTile}\")"),
        };
    }
    public static string Write(Tile tile) {
        return tile.GetId() switch {
            0 => "0",
            1 => "1",
            2 => $"2({(int)((Angle)tile).Dir})",
            3 => $"3({string.Join('-', from dir in ((Start)tile).Dirs select (int)dir)})",
            4 => "4",
            5 => "5",
            6 => "6",
            7 => $"7({((Portal)tile).PortalId}-{((Portal)tile).PairPos["x"]}-{((Portal)tile).PairPos["y"]})",
            8 => $"8({(int)((Blocker)tile).Dir})",
            9 => $"9({(int)((Arrow)tile).Dir})",
            10 => $"10({(int)((Tunnel)tile).Dir})",
            11 => $"11({((FragileBlock)tile).Count})",
            12 => $"12({((GhostBlock)tile).Count})",
            13 => $"13({(int)((FragileAngle)tile).Dir}-{((FragileAngle)tile).Count})",
            14 => $"14({(int)((GhostAngle)tile).Dir}-{((GhostAngle)tile).Count})",
            15 => "15",
            16 => "16",
            _ => throw new ArgumentException($"Failed to write Tile in TileCoDec: invalid Tile \"{tile.GetType()}\""),
        };
    }
}