using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Utils;

public class InputUtils
{
    private static float _sensibility = GameConstants.Constants.Config.InputSensibility;

    public static bool IsKeyLeft()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Left) || gamepadPly1.ThumbSticks.Left.X < -_sensibility || gamepadPly1.DPad.Left == ButtonState.Pressed;
    }

    public static bool IsKeyRight()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Right) || gamepadPly1.ThumbSticks.Left.X > _sensibility || gamepadPly1.DPad.Right == ButtonState.Pressed;
    }

    public static bool IsKeyUp()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Up) || gamepadPly1.ThumbSticks.Left.Y > _sensibility || gamepadPly1.DPad.Up == ButtonState.Pressed;
    }

    public static bool IsKeyDown()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Down) || gamepadPly1.ThumbSticks.Left.Y < -_sensibility || gamepadPly1.DPad.Down == ButtonState.Pressed;
    }

    public static bool IsKeyEscape()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Escape) || gamepadPly1.Buttons.Back == ButtonState.Pressed;
    }

    public static bool IsKeyEnter()
    {
        var (keyboard, gamepadPly1) = GetStates();
        return keyboard.IsKeyDown(Keys.Enter) || gamepadPly1.Buttons.Start == ButtonState.Pressed;
    }

    private static (KeyboardState keyboard, GamePadState gamepadPly1) GetStates()
    {
        return (Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));
    }
}