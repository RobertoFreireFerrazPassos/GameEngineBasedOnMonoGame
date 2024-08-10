using GameEngine.Elements.Sprites;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static GameEngine.Enums.Constants;

namespace GameEngine.Elements;

internal class Player : Object
{
    public Player(int x, int y, int speed, AnimatedSprite sprite) : base(x, y, speed, sprite)
    {
    }

    public override void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            X -= 1 * Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            X += 1 * Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            Y -= 1 * Speed;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            Y += 1 * Speed;
        }
    }

    public override void Draw(SpriteBatch batch)
    {
        (int x, int y) = SpriteUtils.ConvertNumberToXY(AnimatedSprite);
        batch.Draw(AnimatedSprite.Texture.Texture2D, new Vector2(X, Y), new Rectangle(x * Sprite.Pixels, y * Sprite.Pixels, Sprite.Pixels, Sprite.Pixels), AnimatedSprite.Color);
    }
}
