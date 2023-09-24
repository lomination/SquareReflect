using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class BoardCoDec {
    public static void SaveAs<T>(string path, Board<T> board) where T : Tile {
        string[] encodedGrid = new string[board.Size[1]];
        for (int l = 0; l < board.Size[1]; l++) {
            string encodedLine = "";
            (T tile, int number) currentTile = (board.Grid[l][0], 0);
            foreach (T tile in board.Grid[l]) {
                if (tile.Equals(currentTile.tile)) {
                    currentTile.number += 1;
                } else {
                    if (currentTile.number > 1) {
                        encodedLine += $"{currentTile.number}*{TileCoDec.Write(currentTile.tile)},";
                    } else {
                        encodedLine += TileCoDec.Write(currentTile.tile) + ',';
                    }
                    currentTile = (tile, 1);
                }
            }
            if (currentTile.number > 1) {
                if (currentTile.tile.Equals(board.DefaultTile)) {
                    encodedLine += $"{currentTile.number}*{TileCoDec.Write(currentTile.tile)}";
                }
            } else {
                encodedLine += TileCoDec.Write(currentTile.tile);
            }
            encodedGrid[l] = encodedLine;
        }
        BoardStruct boardStruct = new(
            board.Version,
            board.Title,
            board.Author,
            board.Date,
            board.Difficulty,
            board.Size,
            TileCoDec.Write(board.DefaultTile),
            TileCoDec.Write(board.BoarderTile),
            encodedGrid
        );
        File.WriteAllText(path, JsonConvert.SerializeObject(boardStruct, Formatting.Indented));
    }
    public static Board<Tile> Load(string path) {
        JsonTextReader reader = new(File.OpenText(path));
        JObject board = (JObject)JToken.ReadFrom(reader);
        string version = board["version"]!.ToObject<string>()!;
        if (version == "1.0") {
            List<Tile[]> grid = new();
            foreach (JToken line in board["grid"]!.ToObject<JArray>()!) {
                string lineBis = line.ToObject<string>()!;
                List<Tile> newLine = new();
                foreach (string tile in lineBis.Split(',', StringSplitOptions.RemoveEmptyEntries)) {
                    if (tile.Contains('*')) {
                        if (int.TryParse(tile[..tile.IndexOf('*')], out int count)) {
                            Tile currentTile = TileCoDec.Read(tile[(tile.IndexOf('*') + 1)..]);
                            for (int i = 0; i < count; i++) {
                                newLine.Add(currentTile.Clone());
                            }
                        } else {
                            throw new ArgumentException($"Invalid token : * must be placed after an int. Given: {tile}");
                        }
                    } else {
                        newLine.Add(TileCoDec.Read(tile));
                    }
                }
                grid.Add(newLine.ToArray());
            }
            return new Board<Tile>(
                version,
                board["title"] is null ? null : board["title"]!.ToObject<string>()!,
                board["author"] is null ? null : board["author"]!.ToObject<string>()!,
                board["date"] is null ? null : board["date"]!.ToObject<string>()!,
                board["difficulty"] is null ? null : board["difficulty"]!.ToObject<string>()!,
                board["size"]!.ToObject<JArray>()!.Select(j => j.ToObject<int>()).ToArray(),
                TileCoDec.Read(board["default-tile"]!.ToObject<string>()!),
                TileCoDec.Read(board["border-tile"]!.ToObject<string>()!),
                grid.ToArray()
            );
        } else {
            throw new Exception("Board version not supported");
        }
    }
}