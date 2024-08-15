using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers;

public static class TextureManager
{
    public static Elements.Texture Texture { get; set; }

    private static ContentManager Content;

    public static void LoadTextureManager(ContentManager content)
    {
        content.RootDirectory = GameConstants.Constants.Config.Content;
        Content = content;
    }

    public static void AddTexture(string textureKey, int rows, int columns, int pixels)
    {
        Texture = new Elements.Texture()
        {
            Texture2D = Content.Load<Texture2D>(textureKey),
            Rows = rows,
            Columns = columns,
            Pixels = pixels
        };
    }
}