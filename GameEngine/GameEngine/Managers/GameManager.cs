using GameEngine.Enums;
using GameEngine.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Managers;

internal class GameManager
{
    private SpriteManager _spriteManager;
    private TextureManager _textureManager;
    private Node _rootNode = new Node();

    public GameManager(Game1 game, ContentManager content)
    {
        _spriteManager = new SpriteManager(game);
        _textureManager = new TextureManager(content);
    }

    public void LoadContent(GraphicsDevice graphicsDevice)
    {
        _spriteManager.LoadSpriteBatch(graphicsDevice);
        _textureManager.AddTexture(Constants.Sprite.Coin);
        LoadGame();
    }

    private void LoadGame()
    {
        var coin1 = new Node();
        coin1.Nodes.Add(Constants.Node.AnimatedCoin, new AnimatedSprite(
                _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Coin), 
                new Vector2(100, 100), 
                Color.White));
        var coin2 = new Node();
        coin2.Nodes.Add(Constants.Node.AnimatedCoin, new AnimatedSprite(
                _textureManager.Textures.GetValueOrDefault(Constants.Sprite.Coin),
                new Rectangle(100, 164, 256, 64),
                Color.White));

        _rootNode.Nodes.Add(Constants.Node.Coin1, coin1);
        _rootNode.Nodes.Add(Constants.Node.Coin2, coin2);
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(GameTime gameTime)
    {
        var batch = _spriteManager.SpriteBatch;

        batch.Begin();

        foreach (var nodeKeyValuePair in _rootNode.Nodes)
        {
            // TODO: create an optimized way to draw animations. _rootNode.Nodes is a tree structure

            var coinAnimatedSprite = (AnimatedSprite) nodeKeyValuePair.Value.Nodes.GetValueOrDefault(Constants.Node.AnimatedCoin);
            coinAnimatedSprite.Draw(batch);
        }

        batch.End();
    }
}
