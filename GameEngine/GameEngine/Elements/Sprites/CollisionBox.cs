﻿namespace GameEngine.Elements.Sprites;

public class CollisionBox
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public CollisionBox(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width  = width; 
        Height = height;
    }
}
