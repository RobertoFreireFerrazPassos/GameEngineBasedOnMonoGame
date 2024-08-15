using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements.Managers;

public static class TextureManager
{
    public static Texture Texture { get; set; }

    private static ContentManager Content;

    public static void LoadTextureManager(ContentManager content)
    {
        content.RootDirectory = GameConstants.Constants.Config.Content;
        Content = content;
    }

    public static void AddTexture(string textureKey, int rows, int columns, int pixels)
    {
        Texture = new Texture()
        {
            Texture2D = Content.Load<Texture2D>(textureKey),
            Rows = rows,
            Columns = columns,
            Pixels = pixels
        };
    }
}