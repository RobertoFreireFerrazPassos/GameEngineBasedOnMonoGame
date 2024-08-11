using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using GameEngine.Enums;

namespace GameEngine.Elements;

internal class Enemy : Object
{
    public Enemy(int x, int y, Texture texture) : base(x, y)
    {
        Speed = 2;

        var animations = new Dictionary<AnimationEnum, Sprites.Animation>
            {
                {
                    AnimationEnum.IDLE,
                    new Sprites.Animation()
                    {
                        Frames = new int[] { 6 },
                        FrameDuration = TimeSpan.FromMilliseconds(300),
                        Loop = false
                    }
                }
            };

        AnimatedSprite = new AnimatedSprite(
                    texture
                    , Color.White
                    , animations
                    , AnimationEnum.IDLE
                );
        CollisionBox = new CollisionBox(6, 17, 28, 18);
    }

    public void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
        base.Draw(batch, gameTime);
    }
}
