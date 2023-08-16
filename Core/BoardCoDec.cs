using System.Text.RegularExpressions;

public static class BoardCoDec {
    public static string Encode<T>(Board<T> board, bool oneLine = true, bool useCoefficient = true) where T : Tile {
        string encodedGrid = "";
        if (useCoefficient) {
            foreach (T[] line in board.Grid) {
                string encodedLine = "";
                (T tile, int number) currentTile = (line[0], 0);
                foreach (T tile in line) {
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
                    encodedLine += $"{currentTile.number}*{TileCoDec.Write(currentTile.tile)};";
                } else {
                    encodedLine += TileCoDec.Write(currentTile.tile) + ';';
                }
                encodedGrid += "\n\t\t" + encodedLine;
            }
        } else {
            foreach (T[] line in board.Grid) {
                encodedGrid += "\n\t\t" + string.Join(',', from tile in line select TileCoDec.Write(tile));
            }
        }
        string encodedBoard = $"{{\n\ttitle:{{{board.Title}}};\n\tauthor:{{{board.Author}}};\n\tdate:{{{board.Date}}};\n\tdifficulty:{{{board.Difficulty}}};\n\tversion:{{{board.Version}}};\n\tgrid:{{{encodedGrid}\n\tdefault:{{{board.Default}}}}};\n}}";
        if (oneLine) {
            return encodedBoard.Replace("\n", null).Replace("\t", null);
        } else {
            return encodedBoard;
        }
    }
    public static void SaveAs<T>(string path, Board<T> board, bool oneLine = true, bool useCoefficient = true) where T : Tile {
        File.WriteAllText(path, Encode(board, oneLine, useCoefficient));
    }
    public static Board<Tile> Parse(string board) {
        Regex boardRegex = new(
            @"^{title:{(.+)};author:{(.+)};date:{([0-9-]+|unknown)};difficulty:{([0-9]+|unknown)};version:{([0-9.]+|unknown)};grid:{(.+)};defaultTile:{(.+)};}$",
            RegexOptions.Compiled
        );
        Regex multiTileRegex = new(
            @"^(\d+)\*(.+)$",
            RegexOptions.Compiled
        );
        Match boardMatch = boardRegex.Match(board.Replace(" ", null).Replace("\n", null).Replace("\t", null));
        if (boardMatch.Length == 0) {
            throw new ArgumentException($"trop pourri ton board");
        }
        List<Tile[]> newGrid = new();
        foreach (string line in boardMatch.Groups[6].Value.Split(';', StringSplitOptions.RemoveEmptyEntries)) {
            List<Tile> newLine = new();
            foreach (string tile in line.Split(',')) {
                if (tile.Contains('*')) {
                    Match multiTileMatch = multiTileRegex.Match(tile);
                    if (int.TryParse(multiTileMatch.Groups[1].Value, out int count)) {
                        Tile currentTile = TileCoDec.Read(multiTileMatch.Groups[2].Value);
                        for (int i = 0; i < count; i++) {
                            newLine.Add(currentTile.Clone());
                        }
                    } else {
                        throw new ArgumentException($"Invalid token : * must be placed after an int. Given: {multiTileMatch.Groups[1]}");
                    }
                } else {
                    newLine.Add(TileCoDec.Read(tile));
                }
            }
            newGrid.Add(newLine.ToArray());
        }
        return new Board<Tile>(
            boardMatch.Groups[1].Value,
            boardMatch.Groups[2].Value,
            boardMatch.Groups[3].Value,
            boardMatch.Groups[4].Value,
            boardMatch.Groups[5].Value,
            newGrid.ToArray(),
            TileCoDec.Read(boardMatch.Groups[7].Value)
        );
    }
    public static Board<Tile> Load(string path) {
        return Parse(File.ReadAllText(path));
    }
}