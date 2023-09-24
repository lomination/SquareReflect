using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

public class ThemeManager {
    private readonly GraphicsDevice graphics;
    private readonly Dictionary<string, Theme> themeLibrary;
    private Color color;
    public Color Color {
        get => color;
        set => color = value;
    }
    private string currentTheme;
    public string CurrentTheme {
        get => currentTheme;
        set => currentTheme = value;
    }
    public ThemeManager(ContentManager content, GraphicsDevice graphics, Color color) {
        this.graphics = graphics;
        themeLibrary = new Dictionary<string, Theme>() {
            {"", Theme.GetDefaultTheme(content, graphics)}
        };
        currentTheme = "";
        this.color = color;
    }
    public ThemeManager(ContentManager content, GraphicsDevice graphics) : this(content, graphics, Color.White) {}
    public Texture2D GetTexture() {
        return themeLibrary[currentTheme].GetTexture();
    }
    public void LoadTheme(string name) {
        if (File.Exists($"data/themes/{name}")) {
            themeLibrary.Add(name, new Theme(name, graphics));
        }
    }
}