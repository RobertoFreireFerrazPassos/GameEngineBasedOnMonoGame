using GameEngine.Elements.Managers;
using GameEngine.Enums;
using GameEngine.GameObjects.Managers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Game1 : Game
    {
        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            GlobalManager.GraphicsDeviceManager = graphics;
            Content.RootDirectory = GameConstants.Constants.Config.Content;
            GlobalManager.Content = Content;
            GlobalManager.Scene = SceneEnum.START;
            GlobalManager.Scenes.Add(SceneEnum.START, new StartManager());
            GlobalManager.Scenes.Add(SceneEnum.MENU, new MenuManager());
            GlobalManager.Scenes.Add(SceneEnum.GAME, new GameManager());            

            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GlobalManager.GraphicsDevice = GraphicsDevice;
            TextureManager.AddTexture("Sprite-0001");
            SpriteManager.LoadSpriteBatch("Fonts/8bitOperatorPlus-Bold");
            GlobalManager.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputUtils.IsKeyDown(InputEnum.ESCAPE))
                Exit();

            GlobalManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GlobalManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
