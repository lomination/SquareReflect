using System.Text.RegularExpressions;

public class Board<T> where T : Tile {
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
    private readonly T[][] grid;
    public T[][] Grid {get => (from line in grid select (from tile in line select (T)tile.Clone()).ToArray()).ToArray();}
    private T outOfBoardTile;
    public T OutOfBoardTile {
        get { return (T)outOfBoardTile.Clone(); }
        set { outOfBoardTile = value; }
    }
    public Board(string title, string author, string date, string difficulty, string version, T[][] grid, T outOfBoardTile) {
        this.title = title;
        this.author = author;
        this.date = date;
        this.difficulty = difficulty;
        this.version = version;
        this.grid = grid;
        this.outOfBoardTile = outOfBoardTile;
    }
    public Board(T[][] grid, T outOfBoardTile) {
        title = "unknow";
        author = "unknow";
        date = "unknow";
        difficulty = "unknow";
        version = "unknow";
        this.grid = grid;
        this.outOfBoardTile = outOfBoardTile;
    }
    public override string ToString() {
        return string.Join("\n", from line in grid select string.Join("", from tile in line select tile.ToString()));
    }
    public Board<T> Clone() {
        return new Board<T>(
            title,
            author,
            date,
            difficulty,
            version,
            (from line in grid select (from tile in line select (T)tile.Clone()).ToArray()).ToArray(),
            outOfBoardTile
        );
    }
    public T this[int x, int y] {
        get {
            try {
                return (T)grid[y][x].Clone();
            } catch (IndexOutOfRangeException) {
                return outOfBoardTile;
            }
        }
    }
    public T this[Position pos] {
        get => this[pos["x"], pos["y"]];
    }
    public int GetXSize() {
        return grid[0].Count();
    }
    public int GetYSize() {
        return grid.Count();
    }
    public string Encode(ITileCoDec<T> iTileCoDec, bool oneLine = true, bool useCoefficient = true) {
        string encodedGrid = "";
        if (useCoefficient) {
            foreach (T[] line in grid) {
                string encodedLine = "";
                (T tile, int number) currentTile = (line[0], 0);
                foreach (T tile in line) {
                    if (tile.Equals(currentTile.tile)) {
                        currentTile.number += 1;
                    } else {
                        if (currentTile.number > 1) {
                            encodedLine += $"{currentTile.number}*{iTileCoDec.Write(currentTile.tile)},";
                        } else {
                            encodedLine += iTileCoDec.Write(currentTile.tile) + ',';
                        }
                        currentTile = (tile, 1);
                    }
                }
                if (currentTile.number > 1) {
                    encodedLine += $"{currentTile.number}*{iTileCoDec.Write(currentTile.tile)};";
                } else {
                    encodedLine += iTileCoDec.Write(currentTile.tile) + ';';
                }
                encodedGrid += "\n\t\t" + encodedLine;
            }
        } else {
            foreach (T[] line in grid) {
                encodedGrid += "\n\t\t" + string.Join(',', from tile in line select iTileCoDec.Write(tile));
            }
        }
        string encodedBoard = $"{{\n\ttitle:{{{title}}};\n\tauthor:{{{author}}};\n\tdate:{{{date}}};\n\tdifficulty:{{{difficulty}}};\n\tversion:{{{version}}};\n\tgrid:{{{encodedGrid}\n\t}};\n}}";
        if (oneLine) {
            return encodedBoard.Replace("\n", null).Replace("\t", null);
        } else {
            return encodedBoard;
        }
    }
    public static Board<T> Parse(string board, ITileCoDec<T> iTileCoDec) {
        Regex boardRegex = new(
            @"^{title:{(.+)};author:{(.+)};date:{([0-9-]+|unknown)};difficulty:{([0-9]+|unknown)};version:{([0-9.]+|unknown)};grid:{(.+)};outOfBoardTile:{(.+)};}$",
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
        List<T[]> newGrid = new();
        foreach (string line in boardMatch.Groups[6].Value.Split(';', StringSplitOptions.RemoveEmptyEntries)) {
            List<T> newLine = new();
            foreach (string tile in line.Split(',')) {
                if (tile.Contains('*')) {
                    Match multiTileMatch = multiTileRegex.Match(tile);
                    if (int.TryParse(multiTileMatch.Groups[1].Value, out int count)) {
                        T currentTile = iTileCoDec.Read(multiTileMatch.Groups[2].Value);
                        for (int i = 0; i < count; i++) {
                            newLine.Add((T)currentTile.Clone());
                        }
                    } else {
                        throw new ArgumentException($"Invalid token : * must be placed after an int. Given: {multiTileMatch.Groups[1]}");
                    }
                } else {
                    newLine.Add(iTileCoDec.Read(tile));
                }
            }
            newGrid.Add(newLine.ToArray());
        }
        return new Board<T>(
            boardMatch.Groups[1].Value,
            boardMatch.Groups[2].Value,
            boardMatch.Groups[3].Value,
            boardMatch.Groups[4].Value,
            boardMatch.Groups[5].Value,
            newGrid.ToArray(),
            iTileCoDec.Read(boardMatch.Groups[7].Value)
        );
    }
    public void SaveAs(string path, ITileCoDec<T> iTileCoDec, bool oneLine = true, bool useCoefficient = true) {
        File.WriteAllText(path, Encode(iTileCoDec, oneLine, useCoefficient));
    }
    public static Board<T> Load(string path, ITileCoDec<T> iTileCoDec) {
        return Parse(File.ReadAllText(path), iTileCoDec);
    }
}
