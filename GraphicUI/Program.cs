public class Program {
    public static void Main(string[]? boardName) {
        var game = new GraphicUI.App(string.Join(' ', boardName!));
        game.Run();
    }
}


// using var game = new GraphicUI.Game1();
// game.Run();
