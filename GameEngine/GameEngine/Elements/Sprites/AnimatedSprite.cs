using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements.Sprites;

internal class AnimatedSprite
{
    public Texture Texture { get; set; }

    public Vector2 Vector2 { get; set; }

    public Rectangle Source { get; set; }

    public Color Color { get; set; }

    public Visibility Visibility { get; set; } = new Visibility();

    public Order Ordering { get; set; } = new Order();

    public AnimatedSprite(Texture texture, Vector2 vector, int number, Color color)
    {
        Texture = texture;
        Vector2 = vector;
        (int x, int y) = SpriteUtils.ConvertNumberToXY(number, Texture.Rows, Texture.Columns);
        Source = new Rectangle(x * Constants.Sprite.Pixels, y * Constants.Sprite.Pixels, Constants.Sprite.Pixels, Constants.Sprite.Pixels);
        Color = color;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture.Texture2D, Vector2, Source, Color);
    }
}