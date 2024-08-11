using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.GameConstants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static GameEngine.GameConstants.Constants;

namespace GameEngine.Managers;

internal class StartManager : ISceneManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private SceneManager _sceneManager; 
    private float _timer;
    private float _opaque = 1f;
    private float _freezeTime = 3f;
    private float _fadeoutTime = 3f;
    private int _imageNumber = 1;
    private int _screenWidth = 0;
    private int _screenHeight = 0;

    public StartManager(GraphicsDeviceManager graphicsDeviceManager, SpriteManager spriteManager, TextureManager textureManager, SceneManager sceneManager)
    {
        _spriteManager = spriteManager;
        _textureManager = textureManager;
        _sceneManager = sceneManager;
        _screenWidth = graphicsDeviceManager.PreferredBackBufferWidth;
        _screenHeight = graphicsDeviceManager.PreferredBackBufferHeight;
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        if (_imageNumber == 1)
        {
            firstImage(gameTime);
        }
        else if (_imageNumber == 2)
        {
            LastImage(gameTime);
        }
    }

    private void firstImage(GameTime gameTime)
    {
        var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += seconds;

        if (_timer > _freezeTime)
        {
            ApplyFadeOutEffect(seconds);
        }

        if (_timer > _freezeTime + _fadeoutTime)
        {
            _timer = 0f;
            _opaque = 1f;
            _imageNumber = 2;
        }
    }

    private void LastImage(GameTime gameTime)
    {
        var seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += seconds;

        if (_timer > _freezeTime)
        {
            ApplyFadeOutEffect(seconds);
        }

        if (_timer > _freezeTime + _fadeoutTime)
        {
            NextScreen();
        }
    }

    private void NextScreen()
    {
        _sceneManager.Scene = SceneEnum.MENU;
        _timer = 0f;
        _opaque = 1f;
    }

    private void ApplyFadeOutEffect(float seconds)
    {
        _opaque -= seconds / (_fadeoutTime);
    }

    public void Draw(GameTime gameTime)
    {
        var color = new Color(255, 0, 0, 255) * _opaque;
        var batch = _spriteManager.SpriteBatch;
        batch.Begin(samplerState: SamplerState.PointClamp);

        if (_imageNumber == 1)
        {
            DrawFirstImage(batch, color);
        }
        else if (_imageNumber == 2)
        {
            DrawLastImage(batch, color);
        }

        batch.End();
    }

    private void DrawFirstImage(SpriteBatch batch, Color color)
    {
        var pixels = Constants.Sprite.Pixels;
        var texture = _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Sprites);
        (int x, int y) = SpriteManager.ConvertNumberToXY(40, texture.Rows, texture.Columns);

        batch.Draw(
            texture.Texture2D,
            new Vector2(_screenWidth / 2 - 160, _screenHeight / 2 - 80),
            new Rectangle(x * pixels, y * pixels, 4*pixels, 2*pixels),
            color,
            0,
            new Vector2(1, 1),
            new Vector2(2, 2),
            SpriteEffects.None,
            0f
        );
    }

    private void DrawLastImage(SpriteBatch batch, Color color)
    {
        batch.DrawString(_spriteManager.Font, "PRIEST VS DEMONS", new Vector2(_screenWidth / 2 - 50, _screenHeight / 2 - 50), color);
        batch.DrawString(_spriteManager.Font, "BY ROBERTO FREIRE", new Vector2(_screenWidth / 2 - 50, _screenHeight / 2), color);
    }
}
