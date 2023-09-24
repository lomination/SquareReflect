using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class Theme {
    private readonly string name;
    public string Name { get => name; }
    private readonly Texture2D texture;
    private readonly GraphicsDevice graphics;
    public Theme(string name, GraphicsDevice graphics) {
        if (File.Exists($"data/themes/{name}")) {
            this.name = name;
            texture = Texture2D.FromFile(graphics, $"data/themes/{name}");
            this.graphics = graphics;
        } else {
            throw new Exception("theme not found");
        }
    }
    private Theme(ContentManager content, GraphicsDevice graphics) {
        name = "";
        texture = content.Load<Texture2D>("DefaultTheme");
        this.graphics = graphics;
    }
    public static Theme GetDefaultTheme(ContentManager content, GraphicsDevice graphics) {
        return new Theme(content, graphics);
    }
    public Texture2D GetTexture() {
        return texture;
    }
}
