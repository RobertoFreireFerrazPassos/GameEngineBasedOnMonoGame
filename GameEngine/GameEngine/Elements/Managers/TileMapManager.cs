using GameEngine.Elements.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Elements.Managers;

public static class TileMapManager
{
    public static Dictionary<string,TileMap> TileMaps = new Dictionary<string, TileMap>();

    public static List<Tile> Tiles;

    public static int Pixels;

    public static void LoadTileMap(List<Tile> tiles, int pixels)
    {
        Tiles = tiles;
        Pixels = pixels;
    }

    public static void AddTileMap(string name, string filePath, uint positionX, uint positionY)
    {
        var tileMap = new TileMap()
        {
            Position = new Vector2(positionX, positionY),            
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
                        tileMap.Map[new Vector2(x, y)] = value;
                    }
                }
            }

            y++;
        }

        TileMaps.Add(name, tileMap);
    }

    public static void Draw(SpriteBatch batch, GameTime gameTime)
    {
        var offset = Camera.Position;

        foreach (var map in TileMaps)
        {
            foreach (var tileItem in map.Value.Map)
            {
                var dest = GetTileRectangle(map.Value, tileItem.Key, (int)offset.X, (int)offset.Y);
                var src = Tiles[tileItem.Value - 1].Texture;
                batch.Draw(
                    TextureManager.Texture.Texture2D,
                    dest,
                    src,
                    Color.White
                );
            }
        }
    }

    public static bool IsCollidingWithTiles(Rectangle playerRect)
    {
        foreach (var map in TileMaps)
        {
            foreach (var tileItem in map.Value.Map)
            {
                var tileRect = GetTileRectangle(map.Value, tileItem.Key);
                if (tileRect.Intersects(playerRect))
                {
                    if (!Tiles[tileItem.Value - 1].Collidable)
                    {
                        continue;
                    }
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsCollidingWithTiles(Vector2 position)
    {
        foreach (var map in TileMaps)
        {
            foreach (var tileItem in map.Value.Map)
            {
                var tileRect = GetTileRectangle(map.Value, tileItem.Key);
                if (tileRect.Contains(position))
                {
                    if (!Tiles[tileItem.Value - 1].Collidable)
                    {
                        continue;
                    }
                    return true;
                }
            }
        }

        return false;
    }

    private static Rectangle GetTileRectangle(TileMap map, Vector2 tilePosition, int offSetX = 0, int offSetY = 0)
    {
        return new Rectangle(
            (int)map.Position.X + (int)tilePosition.X * Pixels + offSetX,
            (int)map.Position.Y + (int)tilePosition.Y * Pixels + offSetY,
            Pixels,
            Pixels
        );
    }
}
