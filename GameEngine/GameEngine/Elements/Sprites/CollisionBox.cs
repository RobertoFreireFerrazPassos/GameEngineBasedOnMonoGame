using System.Drawing;

namespace GameEngine.Elements.Sprites;

internal class CollisionBox
{
    private int X;
    private int Y;
    private int Width;
    private int Height;

    public CollisionBox(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width  = width; 
        Height = height;
    }

    public Rectangle GetBox(int positionX, int positionY)
    {
        return new Rectangle(positionX + X, positionY + Y, Width, Height);
    }
}
