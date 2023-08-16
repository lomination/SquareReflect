public interface ITileCoDec<T> where T : Tile {
    public T Read(string encodedTile);
    public string Write(T tile);
}