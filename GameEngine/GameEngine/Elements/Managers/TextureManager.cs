﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements.Managers;

public static class TextureManager
{
    public static Texture2D Texture2D;

    private static ContentManager Content;

    public static void LoadTextureManager(ContentManager content)
    {
        content.RootDirectory = GameConstants.Constants.Config.Content;
        Content = content;
    }

    public static void AddTexture(string textureKey)
    {
        Texture2D = Content.Load<Texture2D>(textureKey);
    }
}