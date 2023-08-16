public interface IController<T> where T : Tile {
    public (int playerId, Status newDirection)? GetInput();
    public void Display(SRGame<T> game);
}
