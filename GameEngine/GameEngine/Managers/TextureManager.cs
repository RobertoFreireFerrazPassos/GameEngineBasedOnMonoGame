using GameEngine.Enums;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class TextureManager
{
    public Dictionary<string, Texture2D> Textures { get; set; } = new Dictionary<string, Texture2D>();

    private ContentManager Content;

    public TextureManager(ContentManager content)
    {
        content.RootDirectory = Constants.Config.Content;
        Content = content;
    }

    public void AddTexture(string textureKey)
    {
        Textures.Add(textureKey, Content.Load<Texture2D>(textureKey));
    }
}
