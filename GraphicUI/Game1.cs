using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GraphicUI;

public class Game1 : Game {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private readonly KeyboardManager keyboardManager = new();
    private SRGame<DrawableTile> game;
    private Texture2D playerSprite;
    private double nextRefresh = 0;
    private double refreshRate = 100;
    private readonly int tileSize = 30;
    private readonly GraphicController controller = new();
    private readonly Dictionary<PlayerIndex, Dictionary<Keys, Status>> controls = new() {
        { PlayerIndex.One, new Dictionary<Keys, Status> {
            { Keys.Up, Status.IsGoingUp },
            { Keys.Down, Status.IsGoingDown },
            { Keys.Left, Status.IsGoingLeft },
            { Keys.Right, Status.IsGoingRight }
        } },
        { PlayerIndex.Two, new Dictionary<Keys, Status> {
            { Keys.Z, Status.IsGoingUp },
            { Keys.S, Status.IsGoingDown },
            { Keys.Q, Status.IsGoingLeft },
            { Keys.D, Status.IsGoingRight }
        } }
    };
    public Game1() {
        graphics = new GraphicsDeviceManager(this);
        Window.AllowAltF4 = true;
        Window.AllowUserResizing = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent() {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        playerSprite = Content.Load<Texture2D>("Player");
        Board<Tile> board = BoardCoDec.Load("Boards/ALevel.srboard");
        DrawableTileConverter boardConverter = new(Content);
        game = new SRGame<DrawableTile>(
            boardConverter.ConvertBoard(board),
            controller
        );
    }
    
    protected override void Update(GameTime gameTime) {
        keyboardManager.Update();
        if (keyboardManager.IsKeyHeld(Keys.Escape)) { Exit(); }
        foreach(PlayerIndex playerId in Enum.GetValues(typeof(PlayerIndex))) {
            if ((int)playerId >= game.Players.Length) { break; }
            GamePadState padState = GamePad.GetState(playerId);
            if (padState.Buttons.Back == ButtonState.Pressed) Exit();
            if (padState.ThumbSticks.Left.X < -0.5f) { game.MovePlayer((int)playerId, Status.IsGoingLeft); }
            if (padState.ThumbSticks.Left.X >  0.5f) { game.MovePlayer((int)playerId, Status.IsGoingRight); }
            if (padState.ThumbSticks.Left.Y < -0.5f) { game.MovePlayer((int)playerId, Status.IsGoingUp); } // Maybe down
            if (padState.ThumbSticks.Left.Y >  0.5f) { game.MovePlayer((int)playerId, Status.IsGoingDown); } // Maybe up

            if (controls.ContainsKey(playerId)) {
                foreach(KeyValuePair<Keys, Status> pair in controls[playerId]) {
                    if (keyboardManager.IsKeyPressed(pair.Key)) {
                        game.MovePlayer((int)playerId, pair.Value);
                    }
                }
            }
        }
        if (gameTime.TotalGameTime.TotalMilliseconds > nextRefresh) {
            game.Play(1);
            nextRefresh = gameTime.TotalGameTime.TotalMilliseconds + refreshRate;
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        for (int y = 0; y < game.GetYSize(); y++) {
            for (int x = 0; x < game.GetXSize(); x++) {
                Texture2D texture = game[x, y].GetImage();
                Rectangle position = new(x * tileSize, y * tileSize, tileSize, tileSize);
                spriteBatch.Draw(texture, position, Color.Black);
            }
        }
        foreach(Player p in game.Players) {
            int x = 0;
            int y = 0;
            int delta = (int)((nextRefresh - gameTime.TotalGameTime.TotalMilliseconds) / refreshRate * tileSize);
            switch (p.Status) {
                case Status.IsGoingUp : y = delta; break;
                case Status.IsGoingDown : y = - delta; break;
                case Status.IsGoingLeft : x = delta; break;
                case Status.IsGoingRight : x = - delta; break;
                default : break;
            }
            Rectangle playerPos = new(p.Pos["x"] * tileSize + x, p.Pos["y"] * tileSize + y, tileSize, tileSize);
            spriteBatch.Draw(playerSprite, playerPos, Color.Black);
        }
        spriteBatch.End();
        base.Draw(gameTime);
    }
}
