public interface IController {
    public (int playerId, Status newDirection)? GetInput();
    public void Display(Game game);
}
