using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GraphicUI;

public class Game1 : Game {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private readonly KeyboardManager keyboardManager = new();
    private SRGame<DrawableTile> game;
    private Player[] oldPlayers;
    private Texture2D playerSprite;
    private double lastSRGRefresh = 0;
    private double SRGRefreshRate = 100;
    private readonly int tileSize = 30;
    private readonly GraphicController controller = new();
    private readonly Dictionary<Keys, Status>[] controls = new Dictionary<Keys, Status>[4] {
        new Dictionary<Keys, Status>() {
            { Keys.Up, Status.IsGoingUp },
            { Keys.Down, Status.IsGoingDown },
            { Keys.Left, Status.IsGoingLeft },
            { Keys.Right, Status.IsGoingRight }
        },
        new Dictionary<Keys, Status>() {
            { Keys.Z, Status.IsGoingUp },
            { Keys.S, Status.IsGoingDown },
            { Keys.Q, Status.IsGoingLeft },
            { Keys.D, Status.IsGoingRight }
        },
        new Dictionary<Keys, Status>(),
        new Dictionary<Keys, Status>()
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
        Board<Tile> board = BoardCoDec.Load("Boards/A Level.srboard");
        DrawableTileConverter boardConverter = new(Content);
        game = new SRGame<DrawableTile>(
            boardConverter.ConvertBoard(board),
            controller
        );
        oldPlayers = game.Players.ToArray();
    }
    
    protected override void Update(GameTime gameTime) {
        keyboardManager.Update();
        if (keyboardManager.IsKeyHeld(Keys.Escape)) { Exit(); }
        for (int playerId = 0; playerId < game.Players.Length; playerId++) {
            // keyboard
            foreach(KeyValuePair<Keys, Status> pair in controls[playerId]) {
                if (keyboardManager.IsKeyPressed(pair.Key)) {
                    game.MovePlayer(playerId, pair.Value);
                }
            }
            // joystick
            GamePadState padState = GamePad.GetState(playerId);
            if (padState.Buttons.Back == ButtonState.Pressed) Exit();
            if (padState.ThumbSticks.Left.X < -0.5f) { game.MovePlayer(playerId, Status.IsGoingLeft); }
            if (padState.ThumbSticks.Left.X >  0.5f) { game.MovePlayer(playerId, Status.IsGoingRight); }
            if (padState.ThumbSticks.Left.Y < -0.5f) { game.MovePlayer(playerId, Status.IsGoingUp); } // Maybe down
            if (padState.ThumbSticks.Left.Y >  0.5f) { game.MovePlayer(playerId, Status.IsGoingDown); } // Maybe up
        }
        if (gameTime.TotalGameTime.TotalMilliseconds > lastSRGRefresh + SRGRefreshRate) {
            RefreshSRGame(gameTime);
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin();
        for (int y = 0; y < game.GetYSize(); y++) {
            for (int x = 0; x < game.GetXSize(); x++) {
                DrawableTileDisplay display = game[x, y].GetImage();
                Rectangle position = new(x * tileSize, y * tileSize, tileSize, tileSize);
                spriteBatch.Draw(display.Texture, position, Color.White);
            }
        }
        for (int i = 0; i < game.Players.Length; i++) {
            Position newPos = game.Players[i].Pos;
            Position oldPos = oldPlayers[i].Pos;
            if (Math.Abs(newPos["x"] - oldPos["x"]) + Math.Abs(newPos["y"] - oldPos["y"]) <= 1) {
                int x = 0;
                int y = 0;
                int delta = (int)((gameTime.TotalGameTime.TotalMilliseconds - lastSRGRefresh) / SRGRefreshRate * tileSize + tileSize / 2);
                if (oldPos.Move(Status.IsGoingUp).Equals(newPos)) { y -= delta; }
                else if (oldPos.Move(Status.IsGoingDown).Equals(newPos)) { y += delta; }
                else if (oldPos.Move(Status.IsGoingLeft).Equals(newPos)) { x -= delta; }
                else if (oldPos.Move(Status.IsGoingRight).Equals(newPos)) { x += delta; }
                Rectangle playerPos;
                playerPos = new(oldPos["x"] * tileSize + x, oldPos["y"] * tileSize + y, tileSize, tileSize);
                spriteBatch.Draw(playerSprite, playerPos, Color.White);
            } else {
                RefreshSRGame(gameTime);
            }
        }
        spriteBatch.End();
        base.Draw(gameTime);
    }
    private void RefreshSRGame(GameTime gameTime) {
        oldPlayers = game.Players.ToArray();
        game.Play(1);
        lastSRGRefresh = gameTime.TotalGameTime.TotalMilliseconds;
    }
}
