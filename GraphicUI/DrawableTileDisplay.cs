using Microsoft.Xna.Framework.Graphics;

public class DrawableTileDisplay {
    private readonly Texture2D texture;
    public Texture2D Texture {
        get => texture;
    }
    private readonly float rotation;
    public float Rotation {
        get => rotation;
    }
    public DrawableTileDisplay(Texture2D texture, float rotation) {
        this.texture = texture;
        this.rotation = rotation;
    }
    public DrawableTileDisplay(Texture2D texture) : this(texture, 0f) {}
}
