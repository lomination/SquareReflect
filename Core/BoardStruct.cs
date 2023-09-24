using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public readonly struct BoardStruct {
    private readonly string version;
    private readonly string? title;
    private readonly string? author;
    private readonly string? date;
    private readonly string? difficulty;
    private readonly int[] size;
    [JsonProperty(PropertyName = "default-tile")]
    private readonly string defaultTile;
    [JsonProperty(PropertyName = "border-tile")]
    private readonly string borderTile;
    private readonly string[] grid;
    // v1.0
    [JsonConstructor]
    public BoardStruct(string version, string? title, string? author, string? date, string? difficulty, int[] size, string defaultTile, string borderTile, string[] grid) {
        this.version = version;
        this.title = title;
        this.author = author;
        this.date = date;
        this.difficulty = difficulty;
        this.size = size;
        this.defaultTile = defaultTile;
        this.borderTile = borderTile;
        this.grid = grid;
    }
}