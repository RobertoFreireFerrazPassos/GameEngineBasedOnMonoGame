namespace GameEngine.Enums;

internal static class Constants
{
    internal static class Config
    {
        public static string Content = "Content";
        public static float InputSensibility = 0.1f;
        public static double SixtyFramesASecond = 1.0/60.0;
    }

    internal static class Sprite
    {
        public static string Sprites = "sprites";
        public static int Pixels = 40;
    }

    internal static class Animation
    {
        public static string Idle = "idle";
        public static string Moving = "moving";
        public static string Up = "up";
    }
}
