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
    private T defaultTile;
    public T Default {
        get { return (T)defaultTile.Clone(); }
        set { defaultTile = value; }
    }
    public Board(string title, string author, string date, string difficulty, string version, T[][] grid, T defaultTile) {
        this.title = title;
        this.author = author;
        this.date = date;
        this.difficulty = difficulty;
        this.version = version;
        this.grid = grid;
        this.defaultTile = defaultTile;
    }
    public Board(T[][] grid, T defaultTile) {
        title = "unknow";
        author = "unknow";
        date = "unknow";
        difficulty = "unknow";
        version = "unknow";
        this.grid = grid;
        this.defaultTile = defaultTile;
    }
    public Board<T> Clone() {
        return new Board<T>(
            title,
            author,
            date,
            difficulty,
            version,
            (from line in grid select (from tile in line select (T)tile.Clone()).ToArray()).ToArray(),
            defaultTile
        );
    }
    public T this[int x, int y] {
        get {
            try {
                return (T)grid[y][x].Clone();
            } catch (IndexOutOfRangeException) {
                return defaultTile;
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
}
