using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Elements.Managers;

public static class TextureManager
{
    public static Dictionary<string, Texture2D> Texture2D = new Dictionary<string, Texture2D>();

    public static void AddTexture(string textureKey, string textureFileName)
    {
        Texture2D.Add(textureKey,GlobalManager.Content.Load<Texture2D>(textureFileName));
    }
}