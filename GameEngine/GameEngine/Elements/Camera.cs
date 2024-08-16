﻿using Microsoft.Xna.Framework;

namespace GameEngine.Elements;

public static class Camera
{
    public static Vector2 Position = new Vector2(0, 0);

    public static int Column = 0;

    public static int Row = 0;

    public static int GridSizeWidth;

    public static int GridSizeHeight;

    public static void LoadCamera(int gridSizeWidth, int gridSizeHeight)
    {
        GridSizeWidth = gridSizeWidth;
        GridSizeHeight = gridSizeHeight;
    }

    public static void Update(Vector2 position)
    {
        Column = (int) position.X / GridSizeWidth;
        Row = (int)position.Y / GridSizeHeight;
        Position = new Vector2(- Column * GridSizeWidth,- Row * GridSizeHeight);
    }
}