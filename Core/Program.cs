public class Program {
    public static void Main() {
        Board myBoard = Board.Load("Boards/ALevel.sqb");
        Console.Title = $"SquareReflect : {myBoard.Title} (by {myBoard.Author})";
        Game myGame = new(myBoard, new ConsoleController());
        myGame.Play();
        if (myGame.Players.All(p => p.Status == Status.HasFinished)) {
            Console.WriteLine("You won!");
        } else {
            Console.WriteLine("You lost!");
        }
    }
}
