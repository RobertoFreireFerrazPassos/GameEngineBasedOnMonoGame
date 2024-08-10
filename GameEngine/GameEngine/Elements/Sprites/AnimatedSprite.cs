using Microsoft.Xna.Framework;

namespace GameEngine.Elements.Sprites;

internal class AnimatedSprite
{
    public Texture Texture { get; set; }

    public int Sprite { get; set; }

    public Color Color { get; set; }

    public Visibility Visibility { get; set; } = new Visibility();

    public Order Ordering { get; set; } = new Order();

    public AnimatedSprite(Texture texture, int sprite, Color color)
    {
        Texture = texture;
        Sprite = sprite;
        Color = color;
    }
}