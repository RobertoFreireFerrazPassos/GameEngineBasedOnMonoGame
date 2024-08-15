using GameEngine.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Managers;

public static class TileMapManager
{
    public static Dictionary<Vector2, int> TileMap {  get; set; }

    public static List<Rectangle> TextureStore { get; set; }
    
    public static int Pixels;

    public static void LoadTileMap(string filePath, List<Rectangle> textureStore, int pixels)
    {
        TextureStore = textureStore;
        Pixels = pixels;
        TileMap = new Dictionary<Vector2, int>();

        var reader = new StreamReader(filePath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0;  x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > 0)
                    {
                        TileMap[new Vector2(x, y)] = value;
                    }
                }
            }

            y++;
        }
    }

    public static void Draw(SpriteBatch batch, GameTime gameTime)
    {
        var offset = Camera.Position;
        foreach (var tileItem in TileMap)
        {
            var dest = new Rectangle(
                    (int)tileItem.Key.X * Pixels + (int)offset.X,
                    (int)tileItem.Key.Y * Pixels + (int)offset.Y,
                    Pixels,
                    Pixels
                );

            var src = TextureStore[tileItem.Value - 1];

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
        foreach (var tileItem in TileMap)
        {
            var tileRect = new Rectangle(
                (int)tileItem.Key.X * Pixels,
                (int)tileItem.Key.Y * Pixels,
                Pixels,
                Pixels
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
        foreach (var tileItem in TileMap)
        {
            var tileRect = new Rectangle(
                (int)tileItem.Key.X * Pixels,
                (int)tileItem.Key.Y * Pixels,
                Pixels,
                Pixels
            );

            if (tileRect.Contains(position))
            {
                return true;
            }
        }

        return false;
    }
}
