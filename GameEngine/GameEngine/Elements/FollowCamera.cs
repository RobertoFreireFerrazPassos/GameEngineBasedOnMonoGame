using Microsoft.Xna.Framework;

namespace GameEngine.Elements;

internal static class FollowCamera
{
    public static Vector2 Position;

    public static void LoadFollowCamera(Vector2 position)
    {
        position = position;
    }

    public static void Follow(Rectangle target, Vector2 screenSize)
    {
        Position = new Vector2(
            -target.X + (screenSize.X / 2 - target.Width / 2),
            -target.Y + (screenSize.Y / 2 - target.Height / 2)
        );
    }
}