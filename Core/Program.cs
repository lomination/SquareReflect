public class Program {
    public static void Main() {
        Board myBoard = Board.Load("/home/lomination/Documents/coding/SquareReflect/board1.txt");
        Console.Title = $"SquareReflect : {myBoard.Title} (by {myBoard.Author})";
        Board.SaveAs("/home/lomination/Documents/coding/SquareReflect/board1bis.txt", myBoard, false, true);
        /*
        Game myGame = new Game(myBoard);
        myGame.Play();
        if (myGame.Players.All(p => p.Status == Status.HasFinished)) {
            Console.WriteLine("You won!");
        } else {
            Console.WriteLine("You lost!");
        }
        */
    }
}
