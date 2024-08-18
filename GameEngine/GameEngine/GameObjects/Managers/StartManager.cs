using GameEngine.Elements;
using GameEngine.Elements.Managers;
using GameEngine.Elements.Sprites;
using GameEngine.Enums;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GameObjects.Managers;

public class StartManager : ISceneManager
{
    private ContentManager _content;
    private SoundEffect _introSfx;
    private bool _isIntroSfxPlaying = false;
    private float _introSfxtimer = 2f;
    private float _timer;
    private TweenUtils _fadeOutTween;
    private const float _opaqueDefault = 1f;
    private float _opaque = _opaqueDefault;
    private float _freezeTime = 3f;
    private float _fadeoutTime = 3f;
    private int _imageNumber = 1;
    private int _screenWidth = 0;
    private int _screenHeight = 0;
    private Texture2D _textureSource;
    private Vector2 _screenPosition;
    private Rectangle _sourceRectangle;

    public StartManager(GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
    {
        _screenWidth = graphicsDeviceManager.PreferredBackBufferWidth;
        _screenHeight = graphicsDeviceManager.PreferredBackBufferHeight;
        _content = content;
        _fadeOutTween = new TweenUtils(_opaqueDefault, 0f, _fadeoutTime, EasingFunctions.EaseInQuad);
    }

    public void LoadContent()
    {
        _introSfx = _content.Load<SoundEffect>("Audio/intro");
        _textureSource = TextureManager.Texture2D;
        var texture = new GameEngine.Elements.Texture(40, 26, 13);
        var pixels = texture.Pixels;
        (int x, int y) = texture.ConvertNumberToXY(40);        
        _screenPosition = new Vector2(_screenWidth / 2 - 160, _screenHeight / 2 - 80);
        _sourceRectangle = new Rectangle(x * pixels, y * pixels, 4 * pixels, 2 * pixels);
    }

    public void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += deltaTime;

        if (_imageNumber == 1)
        {
            firstImage(deltaTime);
        }
        else if (_imageNumber == 2)
        {
            LastImage(deltaTime);
        }
    }

    private void firstImage(float deltaTime)
    {
        if (_timer > _introSfxtimer && !_isIntroSfxPlaying)
        {
            _introSfx.Play();
            _isIntroSfxPlaying = true;
        }

        if (_timer > _freezeTime)
        {
            if (_fadeOutTween.Active)
            {
                _opaque = _fadeOutTween.Update(deltaTime);
            }
        }

        if (_timer > _freezeTime + _fadeoutTime)
        {
            _timer = 0f;
            _imageNumber = 2; 
            _opaque = _opaqueDefault;
            _fadeOutTween.Reset();
        }
    }

    private void LastImage(float deltaTime)
    {
        if (_timer > _freezeTime)
        {
            if (_fadeOutTween.Active)
            {
                _opaque = _fadeOutTween.Update(deltaTime);
            }
        }

        if (_fadeOutTween.IsComplete())
        {
            NextScreen();
        }
    }

    private void NextScreen()
    {
        GlobalManager.Scene = SceneEnum.MENU;
        _timer = 0f;
        _opaque = _opaqueDefault;
    }

    public void Draw(GameTime gameTime)
    {
        var color = new Color(255, 0, 0, 255) * _opaque;
        var batch = SpriteManager.SpriteBatch;
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
        batch.Draw(
            _textureSource,
            _screenPosition,
            _sourceRectangle,
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
        batch.DrawString(SpriteManager.Font, "PRIEST VS DEMONS", new Vector2(_screenWidth / 2 - 50, _screenHeight / 2 - 50), color);
        batch.DrawString(SpriteManager.Font, "BY ROBERTO FREIRE", new Vector2(_screenWidth / 2 - 50, _screenHeight / 2), color);
    }
}
