using GameEngine.Elements.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Elements;

internal class Enemy : Object
{
    public Enemy(int x, int y, int speed, AnimatedSprite sprite) : base(x, y, speed, sprite)
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch batch, GameTime gameTime)
    {
    }
}
