using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GraphicUI;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GraphicController controller = new GraphicController();
    private SRGame<DrawableTile> game;
    private readonly Dictionary<string, Texture2D> imgs = new();
    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        game = new SRGame<DrawableTile>(
            DrawableTileConverter.ConvertBoard(BoardCoDec.Load("Boards/ALevel.srboard")),
            controller
        );
    }
    protected override void Initialize() {
        base.Initialize();
    }
    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        string[] imageNames = new string[] {
            "Angle-0",
            "Angle-1",
            "Angle-2",
            "Angle-3",
            "Block",
            "Empty"
        };
        foreach (string img in imageNames) {
            imgs.Add(img, Content.Load<Texture2D>(img));
        }
    }
    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }
        game.Play(1);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        for (int y = 0; y < game.GetYSize(); y++) {
            for (int x = 0; x > game.GetXSize(); x++) {
                foreach (string textureName in game[x, y].DisplayMe()) {
                    if (imgs.ContainsKey(textureName)) {
                        _spriteBatch.Draw(imgs[textureName], new Vector2(x * 10, y * 10), Color.Black);
                    } else {
                        Console.WriteLine("Error : cannot display " + textureName);
                    }
                }
            }
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
