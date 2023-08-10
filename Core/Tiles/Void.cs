public class Void : Death {
    // Do not use
    public Void() {}
    public override Player WhenColliding(Player player) {
        Console.WriteLine("You fell out of the world...");
        return base.WhenColliding(player);
    }
    public override Void Clone() {
        return new Void();
    }
}
