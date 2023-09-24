using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GenericDrawableTile : DrawableTile {
    private readonly int rotation;
    private readonly Func<Tile, bool> condition;
    private readonly int ifTrue;
    private readonly int ifFalse;
    public GenericDrawableTile(Tile tile, ThemeManager themeManager, int rotation, Func<Tile, bool> condition, int ifTrue, int ifFalse) : base(tile, themeManager) {
        this.rotation = rotation;
        this.condition = condition;
        this.ifTrue = ifTrue;
        this.ifFalse = ifFalse;
    }
    public GenericDrawableTile(Tile tile, ThemeManager themeManager, Func<Tile, bool> condition, int ifTrue, int ifFalse) : this(tile, themeManager, 0, condition, ifTrue, ifFalse) {}
    public GenericDrawableTile(Tile tile, ThemeManager themeManager, int themeIndex, int rotation) : this(tile, themeManager, rotation, t => true, themeIndex, 63) {}
    public GenericDrawableTile(Tile tile, ThemeManager themeManager, int themeIndex) : this(tile, themeManager, 0, t => true, themeIndex, 63) {}
    public override GenericDrawableTile Clone() {
        return new GenericDrawableTile(tile.Clone(), themeManager, rotation, condition, ifTrue, ifFalse);
    }
    public override int GetId() {
        return tile.GetId();
    }
    public override bool Equals(object? obj) {
        return tile.Equals(obj);
    }
    public override int GetHashCode() {
        return tile.GetHashCode();
    }
    public override Status[] GetStartDirs() {
        return tile.GetStartDirs();
    }
    public override Player WhenApproching(Player player) {
        return tile.WhenApproching(player);
    }
    public override Player WhenColliding(Player player) {
        return tile.WhenColliding(player);
    }
    public override void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle) {
        Texture2D texture = themeManager.GetTexture();
        Rectangle sourceRectangle = new(
            (condition(tile) ? ifTrue : ifFalse) % 8 * (texture.Width / 8),
            (condition(tile) ? ifTrue : ifFalse) / 8 * (texture.Height / 8),
            texture.Width / 8,
            texture.Height / 8
        );
        if (rotation % 4 == 0) {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, themeManager.Color);
        } else {
            Rectangle newDestinationRectangle = new(
                destinationRectangle.X + (destinationRectangle.Width / 2),
                destinationRectangle.Y + (destinationRectangle.Height / 2),
                destinationRectangle.Width, destinationRectangle.Height
            );
            spriteBatch.Draw(
                texture, newDestinationRectangle, sourceRectangle,
                themeManager.Color, (float)(Math.PI * rotation / 2),
                new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2),
                SpriteEffects.None, 0
            );
        }
    }
}
