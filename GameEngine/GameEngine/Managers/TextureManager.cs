using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class TextureManager
{
    public Dictionary<string, Elements.Texture> Textures { get; set; } = new Dictionary<string, Elements.Texture>();

    private ContentManager Content;

    public TextureManager(ContentManager content)
    {
        content.RootDirectory = GameConstants.Constants.Config.Content;
        Content = content;
    }

    public void AddTexture(string textureKey, int rows, int columns)
    {
        Textures.Add(textureKey, new Elements.Texture()
        {
            Texture2D = Content.Load<Texture2D>(textureKey),
            Rows = rows,
            Columns = columns
        });
    }
}