using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements.Sprites;

internal class AnimatedSprite
{
    public Texture Texture { get; set; }

    public int Sprite { get; set; }

    public Rectangle Source { get; set; }

    public Color Color { get; set; }

    public Visibility Visibility { get; set; } = new Visibility();

    public Order Ordering { get; set; } = new Order();

    public int X { get; set; }

    public int Y { get; set; }

    public AnimatedSprite(Texture texture, int x, int y, int sprite, Color color)
    {
        Texture = texture;
        X = x; 
        Y = y;
        Sprite = sprite;
        Color = color;
    }

    public void Draw(SpriteBatch batch)
    {
        (int x, int y) = SpriteUtils.ConvertNumberToXY(Sprite, Texture.Rows, Texture.Columns);
        Source = new Rectangle(x * Constants.Sprite.Pixels, y * Constants.Sprite.Pixels, Constants.Sprite.Pixels, Constants.Sprite.Pixels);
        batch.Draw(Texture.Texture2D, new Vector2(X, Y), Source, Color);
    }
}