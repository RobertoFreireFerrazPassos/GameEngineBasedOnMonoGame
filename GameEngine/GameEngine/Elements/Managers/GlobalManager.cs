using GameEngine.Enums;
using GameEngine.GameObjects.Managers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Elements.Managers;

public static class GlobalManager
{
    public static SceneEnum Scene { get; set; }

    public static Dictionary<SceneEnum, ISceneManager> Scenes { get; set; } = new Dictionary<SceneEnum, ISceneManager>();

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
