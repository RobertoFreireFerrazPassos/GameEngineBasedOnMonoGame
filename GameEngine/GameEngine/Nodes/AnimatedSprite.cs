using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Nodes;

public class RectangleSprite
{
    public int Type { get; set; }

    public Vector2 Vector2 { get; set; }

    public Rectangle Rectangle { get; set; }
}

internal class AnimatedSprite : Node
{
    public Texture2D Texture { get; set; }

    public RectangleSprite Rectangle { get; set; } = new RectangleSprite();

    public Color Color { get; set; }

    public AnimatedSprite(Texture2D texture, Rectangle rectangle, Color color)
    {
        Texture = texture;
        Rectangle.Type = 0;
        Rectangle.Rectangle = rectangle;
        Color = color;
    }

    public AnimatedSprite(Texture2D texture, Vector2 vector, Color color)
    {
        Texture = texture;
        Rectangle.Type = 1;
        Rectangle.Vector2 = vector;
        Color = color;
    }

    public void Draw(SpriteBatch batch)
    {
        if (Rectangle.Type == 0)
        {
            batch.Draw(Texture, Rectangle.Rectangle, Color);

        }
        else if (Rectangle.Type == 1)
        {
            batch.Draw(Texture, Rectangle.Vector2, Color);
        }
    }
}