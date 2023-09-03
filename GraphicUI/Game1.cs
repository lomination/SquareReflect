using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GraphicUI;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GraphicController controller = new GraphicController();
    private SRGame<DrawableTile> game;
    private Texture2D playerSprite;
    private Dictionary<PlayerIndex, Dictionary<Keys, Status>> keyMap = new Dictionary<PlayerIndex, Dictionary<Keys, Status>>{
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
        }
        }
    };
    public Game1() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent(){
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        playerSprite = Content.Load<Texture2D>("Player");
        var board = BoardCoDec.Load("Boards/ALevel.srboard");
        var boardConverter = new DrawableTileConverter(Content);
         game = new SRGame<DrawableTile>(
            boardConverter.ConvertBoard(board),
            controller
        );
    }
    
    protected override void Update(GameTime gameTime) {
        var keyState = Keyboard.GetState();
        if (keyState.IsKeyDown(Keys.Escape)) Exit();

        foreach(PlayerIndex playerId in Enum.GetValues(typeof(PlayerIndex))) {
            var padState = GamePad.GetState(playerId);
            if (padState.Buttons.Back == ButtonState.Pressed) Exit();
            if (padState.ThumbSticks.Left.X < -0.5f) game.MovePlayer((int) playerId, Status.IsGoingLeft);
            if (padState.ThumbSticks.Left.X >  0.5f) game.MovePlayer((int) playerId, Status.IsGoingRight);
            if (padState.ThumbSticks.Left.Y < -0.5f) game.MovePlayer((int) playerId, Status.IsGoingUp); // Maybe down
            if (padState.ThumbSticks.Left.Y >  0.5f) game.MovePlayer((int) playerId, Status.IsGoingDown); // Maybe up

            if (keyMap.ContainsKey(playerId)) {
                foreach(KeyValuePair<Keys, Status> km in keyMap[playerId]) {
                    if (keyState.IsKeyDown(km.Key))
                        game.MovePlayer((int) playerId, km.Value);
                }
            }
        }
        game.Play(1);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        for (int y = 0; y < game.GetYSize(); y++) {
            for (int x = 0; x < game.GetXSize(); x++) {
                var texture = game[x, y].DisplayMe();
                var position = new Rectangle(x * 10, y * 10, 10, 10);
                _spriteBatch.Draw(texture, position, Color.Black);
            }
        }
        foreach(Player p in game.Players) {
            var playerPos = new Rectangle(p.Pos["x"], p.Pos["y"], 10, 10);
            _spriteBatch.Draw(playerSprite, playerPos, Color.Blue);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
