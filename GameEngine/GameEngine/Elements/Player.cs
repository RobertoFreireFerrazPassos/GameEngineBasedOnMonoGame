using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Elements;

internal class Player : Object
{
    public override void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            AnimatedSprite.X -= 1;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            AnimatedSprite.X += 1;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            AnimatedSprite.Y -= 1;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            AnimatedSprite.Y += 1;
        }
    }
}
