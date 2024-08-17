using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Elements;

public class TileMap
{
    public Vector2 Position { get; set; }

    public Dictionary<Vector2, int> Map { get; set; }

    public List<Rectangle> TextureStore { get; set; }

    public int Pixels;
}
