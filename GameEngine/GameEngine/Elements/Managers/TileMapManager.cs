using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Elements.Managers;

public static class TileMapManager
{
    public static TileMap TileMap { get; set; }

    public static void LoadTileMap(string filePath, uint positionX, uint positionY, List<Rectangle> textureStore, int pixels)
    {
        TileMap = new TileMap()
        {
            Position = new Vector2(positionX, positionY),
            TextureStore = textureStore,
            Pixels = pixels,
            Map = new Dictionary<Vector2, int>()
        };

        var reader = new StreamReader(filePath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > 0)
                    {
                        TileMap.Map[new Vector2(x, y)] = value;
                    }
                }
            }

            y++;
        }
    }

    public static void Draw(SpriteBatch batch, GameTime gameTime)
    {
        var offset = Camera.Position;
        foreach (var tileItem in TileMap.Map)
        {
            var dest = new Rectangle(
                    (int) TileMap.Position.X + (int)tileItem.Key.X * TileMap.Pixels + (int)offset.X,
                    (int) TileMap.Position.Y + (int)tileItem.Key.Y * TileMap.Pixels + (int)offset.Y,
                    TileMap.Pixels,
                    TileMap.Pixels
                );

            var src = TileMap.TextureStore[tileItem.Value - 1];

            batch.Draw(
                TextureManager.Texture.Texture2D,
                dest,
                src,
                Color.White
            );
        }
    }

    public static bool IsCollidingWithTiles(Rectangle playerRect)
    {
        foreach (var tileItem in TileMap.Map)
        {
            var tileRect = new Rectangle(
                (int)TileMap.Position.X + (int)tileItem.Key.X * TileMap.Pixels,
                (int)TileMap.Position.Y + (int)tileItem.Key.Y * TileMap.Pixels,
                TileMap.Pixels,
                TileMap.Pixels
            );

            if (tileRect.Intersects(playerRect))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsCollidingWithTiles(Vector2 position)
    {
        foreach (var tileItem in TileMap.Map)
        {
            var tileRect = new Rectangle(
                (int)TileMap.Position.X + (int)tileItem.Key.X * TileMap.Pixels,
                (int)TileMap.Position.Y + (int)tileItem.Key.Y * TileMap.Pixels,
                TileMap.Pixels,
                TileMap.Pixels
            );

            if (tileRect.Contains(position))
            {
                return true;
            }
        }

        return false;
    }
}
