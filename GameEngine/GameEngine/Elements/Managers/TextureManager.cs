using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements.Managers;

public static class TextureManager
{
    public static Texture2D Texture2D;

    public static void AddTexture(string textureKey)
    {
        Texture2D = GlobalManager.Content.Load<Texture2D>(textureKey);
    }
}