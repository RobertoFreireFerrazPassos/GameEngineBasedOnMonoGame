using GameEngine.Enums;
using GameEngine.Managers;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Game1 : Game
    {
        private GameManager _gameManager;
        private MenuManager _menuManager;
        private StartManager _startManager;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            SpriteManager.LoadSpriteManager(graphics);
            TextureManager.LoadTextureManager(Content);
            _gameManager = new GameManager(graphics);
            _menuManager = new MenuManager();
            _startManager = new StartManager(graphics, Content);
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Remove after finish game
            SceneManager.Scene = SceneEnum.GAME;
            SpriteManager.LoadSpriteBatch(GraphicsDevice, Content.Load<SpriteFont>("Fonts/8bitOperatorPlus-Bold"));
            _startManager.LoadContent();
            _menuManager.LoadContent();
            _gameManager.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputUtils.IsKeyEscape())
                Exit();

            switch(SceneManager.Scene)
            {
                case SceneEnum.START:
                    _startManager.Update(gameTime);
                    break;
                case SceneEnum.MENU:
                    _menuManager.Update(gameTime);
                    break;
                case SceneEnum.GAME:
                    _gameManager.Update(gameTime);
                    break;
            };

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (SceneManager.Scene)
            {
                case SceneEnum.START:
                    _startManager.Draw(gameTime);
                    break;
                case SceneEnum.MENU:
                    _menuManager.Draw(gameTime);
                    break;
                case SceneEnum.GAME:
                    _gameManager.Draw(gameTime);
                    break;
            };

            base.Draw(gameTime);
        }
    }
}
