using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Elements.Managers;

public static class GlobalManager
{
    public static SceneEnum Scene;

    public static GraphicsDeviceManager GraphicsDeviceManager;

    public static GraphicsDevice GraphicsDevice;

    public static ContentManager Content;

    public static Dictionary<SceneEnum, ISceneManager> Scenes = new Dictionary<SceneEnum, ISceneManager>();

    public static void LoadContent()
    {
        foreach (var scene in Scenes)
        {
            scene.Value.LoadContent();
        }
    }

    public static void Update(GameTime gameTime)
    {
        Scenes[Scene].Update(gameTime);
        InputUtils.UpdatePreviousState();
    }

    public static void Draw(GameTime gameTime)
    {
        Scenes[Scene].Draw(gameTime);
    }
}
