public class Program {
    public static void Main(string[]? maybeBoardName) {
        Board<GenericTile> myBoard;
        if (maybeBoardName is not null && File.Exists("Boards/" + string.Join(' ', maybeBoardName) + ".srboard")) {
            myBoard = Board<GenericTile>.Load("Boards/" + string.Join(' ', maybeBoardName) + ".srboard", new GenericTileCoDec());
        } else {
            myBoard = Menu();
        }
        Console.Title = $"SquareReflect : {myBoard.Title} (by {myBoard.Author})";
        SRGame<GenericTile> myGame = new(myBoard, new ConsoleController<GenericTile>());
        myGame.Play();
        if (myGame.Players.All(p => p.Status == Status.HasFinished)) {
            Console.WriteLine("You won!");
        } else {
            Console.WriteLine("You lost!");
        }
    }
    public static Board<GenericTile> Menu() {
        Console.WriteLine("Choose a board (type /help for more information):");
        string? input = Console.ReadLine();
        Console.WriteLine();
        if (input is null) {
            return Menu();
        } else if (input.StartsWith('/')) {
            Console.WriteLine(input switch {
                "/help" => "Enter a board name to play it\n- /list",
                "/list" => "This is the list of the boards:" + string.Join(
                    "\n - ", from boardFile in new DirectoryInfo("./Boards").GetFiles("*.srboard") select boardFile.Name.Remove(boardFile.Name.Length - 8)
                ),
                _ => "Command not found, type /help for more information."
            } + "\n");
            return Menu();
        } else if (File.Exists("Boards/" + input + ".srboard")) {
            return Board<GenericTile>.Load("Boards/" + input + ".srboard", new GenericTileCoDec());
        } else {
            Console.WriteLine("This board does not exist\n");
            return Menu();
        }
    }
}
