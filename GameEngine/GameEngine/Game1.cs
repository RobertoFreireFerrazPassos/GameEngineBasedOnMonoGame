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
        private SceneManager _sceneManager;
        private MenuManager _menuManager;
        private StartManager _startManager;
        private SpriteManager _spriteManager; 
        private TextureManager _textureManager;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            _spriteManager = new SpriteManager(graphics);
            _textureManager = new TextureManager(Content);
            _sceneManager = new SceneManager();
            _gameManager = new GameManager(graphics, _spriteManager, _textureManager, _sceneManager);
            _menuManager = new MenuManager(_spriteManager, _textureManager, _sceneManager);
            _startManager = new StartManager(_spriteManager, _textureManager, _sceneManager);
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteManager.LoadSpriteBatch(GraphicsDevice, Content.Load<SpriteFont>("Fonts/8bitOperatorPlus-Bold"));
            _startManager.LoadContent();
            _menuManager.LoadContent();
            _gameManager.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputUtils.IsKeyEscape())
                Exit();

            switch(_sceneManager.Scene)
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

            switch (_sceneManager.Scene)
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
