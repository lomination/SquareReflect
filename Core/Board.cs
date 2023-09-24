public class Board<T> where T : Tile {
    private readonly string version;
    public string Version {
        get { return version; }
    }
    private readonly string? title;
    public string? Title {
        get { return title; }
    }
    private readonly string? author;
    public string? Author {
        get { return author; }
    }
    private readonly string? date;
    public string? Date {
        get { return date; }
    }
    private readonly string? difficulty;
    public string? Difficulty {
        get { return difficulty; }
    }
    private readonly int[] size;
    public int[] Size {
        get { return size; }
    }
    private readonly T defaultTile;
    public T DefaultTile {
        get { return defaultTile; }
    }
    private readonly T boarderTile;
    public T BoarderTile {
        get { return boarderTile; }
    }
    private readonly T[][] grid;
    public T[][] Grid {get => (from line in grid select (from tile in line select (T)tile.Clone()).ToArray()).ToArray();}
    public Board(string version, string? title, string? author, string? date, string? difficulty, int[] size, T defaultTile, T boarderTile, T[][] grid) {
        this.version = version;
        this.title = title;
        this.author = author;
        this.date = date;
        this.difficulty = difficulty;
        this.size = size;
        this.defaultTile = defaultTile;
        this.boarderTile = boarderTile;
        this.grid = grid;
    }
    public Board(string version, int[] size, T defaultTile, T boarderTile, T[][] grid) : this(version, "unk","unk","unk","unk", size, defaultTile, boarderTile, grid ) {}
    public Board<T> Clone() {
        return new Board<T>(
            version,
            title,
            author,
            date,
            difficulty,
            size,
            defaultTile,
            boarderTile,
            grid.Select(line => line.Select(tile => (T)tile.Clone()).ToArray()).ToArray()
        );
    }
    public T this[int x, int y] {
        get {
            try {
                return grid[y][x];
            } catch (IndexOutOfRangeException) {
                if (x < size[0] && y < size[1]) {
                    return defaultTile;
                } else {
                    return boarderTile;
                }
            }
        }
    }
    public T this[Position pos] {
        get => this[pos["x"], pos["y"]];
    }
    public int[] GetSize() {
        return size;
    }
    public int GetXSize() {
        return size[0];
    }
    public int GetYSize() {
        return size[1];
    }
    public override bool Equals(object? obj) {
        if (obj is null || GetType() != obj.GetType()) {
            return false;
        } else {
            Board<Tile> other = (Board<Tile>)obj;
            if (GetXSize() == other.GetXSize() && GetYSize() == other.GetYSize()) {
                for (int y = 0; y < GetYSize(); y++) {
                    for (int x = 0; x < GetXSize(); x++) {
                        if (!this[x, y].Equals(other[x, y])) {
                            return false;
                        }
                    }
                }
                return true;
            } else {
                return false;
            }
        }
    }
    public override int GetHashCode() {
        int hash = 17;
        for (int y = 0; y < GetYSize(); y++) {
            for (int x = 0; x < GetXSize(); x++) {
                hash = hash * 23 + this[x, y].GetHashCode();
            }
        }
        return hash;
    }
    public Board<T2> Convert<T2>(T2 defaultTile, T2 borderTile, T2[][] grid) where T2 : Tile {
        return new Board<T2>(
            version,
            title,
            author,
            date,
            difficulty,
            size,
            defaultTile,
            borderTile,
            grid
        );
    }
}
