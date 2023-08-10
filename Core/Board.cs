using System.Text.RegularExpressions;

public class Board {
    private string title;
    public string Title {
        get { return title; }
        set { title = value; }
    }
    private string author;
    public string Author {
        get { return author; }
        set { author = value; }
    }
    private string date;
    public string Date {
        get { return date; }
        set { date = value; }
    }
    private string difficulty;
    public string Difficulty {
        get { return difficulty; }
        set { difficulty = value; }
    }
    private string version;
    public string Version {
        get { return version; }
        set { version = value; }
    }
    private readonly Tile[][] grid;
    public Tile[][] Grid {get => (from line in this.grid select (from tile in line select tile.Clone()).ToArray()).ToArray();}
    public Board(string title, string author, string date, string difficulty, string version, Tile[][] grid) {
        this.title = title;
        this.author = author;
        this.date = date;
        this.difficulty = difficulty;
        this.version = version;
        this.grid = grid;
    }
    public Board(Tile[][] grid) {
        title = "unknow";
        author = "unknow";
        date = "unknow";
        difficulty = "unknow";
        version = "unknow";
        this.grid = grid;
    }
    public override string ToString() {
        return string.Join("\n", from line in grid select string.Join("", from tile in line select tile.ToString()));
    }
    public Board Clone() {
        return new Board(
            title,
            author,
            date,
            difficulty,
            version,
            (from line in grid select (from tile in line select tile.Clone()).ToArray()).ToArray()
        );
    }
    public Tile this[int x, int y] {
        get {
            try {
                return grid[y][x].Clone();
            }
            catch (IndexOutOfRangeException) {
                return new Void();
            }
        }
    }
    public Tile this[Position pos] {
        get => this[pos["x"], pos["y"]];
    }
    public int GetXSize() {
        return grid[0].Count();
    }
    public int GetYSize() {
        return grid.Count();
    }
    public string Encode(bool oneLine = true, bool useCoefficient = true) {
        string encodedGrid = "";
        if (useCoefficient) {
            foreach (Tile[] line in grid) {
                string encodedLine = "";
                (Tile tile, int number) currentTile = (line[0], 0);
                foreach (Tile tile in line) {
                    if (tile.Equals(currentTile.tile)) {
                        currentTile.number += 1;
                    } else {
                        if (currentTile.number > 1) {
                            encodedLine += $"{currentTile.number}*{currentTile.tile.Encode()},";
                        } else {
                            encodedLine += currentTile.tile.Encode() + ',';
                        }
                        currentTile = (tile, 1);
                    }
                }
                if (currentTile.number > 1) {
                    encodedLine += $"{currentTile.number}*{currentTile.tile.Encode()};";
                } else {
                    encodedLine += currentTile.tile.Encode() + ';';
                }
                encodedGrid += "\n\t\t" + encodedLine;
            }
        } else {
            foreach (Tile[] line in grid) {
                encodedGrid += "\n\t\t" + string.Join(',', from tile in line select tile.Encode());
            }
        }
        string encodedBoard = $"{{\n\ttitle:{{{title}}};\n\tauthor:{{{author}}};\n\tdate:{{{date}}};\n\tdifficulty:{{{difficulty}}};\n\tversion:{{{version}}};\n\tgrid:{{{encodedGrid}\n\t}};\n}}";
        if (oneLine) {
            return encodedBoard.Replace("\n", null).Replace("\t", null);
        } else {
            return encodedBoard;
        }
    }
    public static Board Parse(string board) {
        Regex boardRegex = new(
            "^{title:{(.+)};author:{(.+)};date:{([0-9-]+)};difficulty:{([0-9]+)};version:{([0-9.]+)};grid:{(.+)};}$",
            RegexOptions.Compiled
        );
        Regex multiTileRegex = new(
            @"^(\d+)\*(.+)$",
            RegexOptions.Compiled
        );
        Match boardMatch = boardRegex.Match(board.Replace(" ", null).Replace("\n", null).Replace("\t", null));
        List<Tile[]> newGrid = new();
        foreach (string line in boardMatch.Groups[6].Value.Split(';', StringSplitOptions.RemoveEmptyEntries)) {
            List<Tile> newLine = new();
            foreach (string tile in line.Split(',')) {
                if (tile.Contains('*')) {
                    Match multiTileMatch = multiTileRegex.Match(tile);
                    if (int.TryParse(multiTileMatch.Groups[1].Value, out int count)) {
                        Tile currentTile = Tile.Parse(multiTileMatch.Groups[2].Value);
                        for (int i = 0; i < count; i++) {
                            newLine.Add(currentTile.Clone());
                        }
                    } else {
                        throw new ArgumentException($"Invalid token : * must be placed after an int. Given: {multiTileMatch.Groups[1]}");
                    }
                } else {
                    newLine.Add(Tile.Parse(tile));
                }
            }
            newGrid.Add(newLine.ToArray());
        }
        return new Board(
            boardMatch.Groups[1].Value,
            boardMatch.Groups[2].Value,
            boardMatch.Groups[3].Value,
            boardMatch.Groups[4].Value,
            boardMatch.Groups[5].Value,
            newGrid.ToArray()
        );
    }
    public static void SaveAs(string path, Board board, bool oneLine = true, bool useCoefficient = true) {
        File.WriteAllText(path, board.Encode(oneLine, useCoefficient));
    }
    public static Board Load(string path) {
        return Parse(File.ReadAllText(path));
    }
}
 