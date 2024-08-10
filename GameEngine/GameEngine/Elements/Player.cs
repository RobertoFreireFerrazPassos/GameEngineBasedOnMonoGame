using GameEngine.Elements.Sprites;
using GameEngine.Enums;
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

    public override void Update(GameTime gameTime)
    {
        AnimatedSprite.FlipHorizontally = SpriteEffects.None;
        var state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Down))
        {
            Y += 1 * Speed;
            AnimatedSprite.SetState(Constants.Animation.Moving);
        }

        if (state.IsKeyDown(Keys.Right))
        {
            X += 1 * Speed;
            AnimatedSprite.SetState(Constants.Animation.Moving);
        }

        if (state.IsKeyDown(Keys.Left))
        {
            X -= 1 * Speed;
            AnimatedSprite.SetState(Constants.Animation.Moving);
            AnimatedSprite.FlipHorizontally = SpriteEffects.FlipHorizontally;
        }

        if (state.IsKeyDown(Keys.Up))
        {
            Y -= 1 * Speed;
            AnimatedSprite.SetState(Constants.Animation.Up);
        }

        if (!state.IsKeyDown(Keys.Down) && !state.IsKeyDown(Keys.Up)
            && !state.IsKeyDown(Keys.Left) && !state.IsKeyDown(Keys.Right))
        {
            AnimatedSprite.SetState(Constants.Animation.Idle);
        }
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
        (int x, int y) = SpriteUtils.ConvertNumberToXY(AnimatedSprite);
        batch.Draw(AnimatedSprite.Texture.Texture2D, new Vector2(X, Y), new Rectangle(x * Sprite.Pixels, y * Sprite.Pixels, Sprite.Pixels, Sprite.Pixels), AnimatedSprite.Color, 0, new Vector2(1, 1), new Vector2(1, 1), AnimatedSprite.FlipHorizontally, 0f);
    }
}
