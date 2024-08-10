using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Nodes;

internal class AnimatedSprite : Node
{
    public Texture2D Texture { get; set; }

    public Vector2 Vector2 { get; set; }

    public Color Color { get; set; }

    public AnimatedSprite(Texture2D texture, Vector2 vector, Color color)
    {
        Texture = texture;
        Vector2 = vector;
        Color = color;
    }
}
