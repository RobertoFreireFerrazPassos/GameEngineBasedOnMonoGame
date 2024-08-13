using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Utils;

public class InputUtils
{
    private static KeyboardState _previousKeyboardState;
    private static GamePadState _previousGamePadState;
    private static float _sensibility = GameConstants.Constants.Config.InputSensibility;

    public static bool IsKeyDown(InputEnum inputkey)
    {
        var keyboard = Keyboard.GetState();
        var gamepadPly1 = GamePad.GetState(PlayerIndex.One);
        var result = false;

        switch (inputkey)
        {
            case InputEnum.LEFT:
                result = keyboard.IsKeyDown(Keys.Left) || gamepadPly1.ThumbSticks.Left.X < -_sensibility || gamepadPly1.DPad.Left == ButtonState.Pressed;
                break;
            case InputEnum.RIGHT:
                result = keyboard.IsKeyDown(Keys.Right) || gamepadPly1.ThumbSticks.Left.X > _sensibility || gamepadPly1.DPad.Right == ButtonState.Pressed;
                break;
            case InputEnum.UP:
                result = keyboard.IsKeyDown(Keys.Up) || gamepadPly1.ThumbSticks.Left.Y > _sensibility || gamepadPly1.DPad.Up == ButtonState.Pressed;
                break;
            case InputEnum.DOWN:
                result = keyboard.IsKeyDown(Keys.Down) || gamepadPly1.ThumbSticks.Left.Y < -_sensibility || gamepadPly1.DPad.Down == ButtonState.Pressed;
                break;
            case InputEnum.ESCAPE:
                result = keyboard.IsKeyDown(Keys.Escape) || gamepadPly1.Buttons.Back == ButtonState.Pressed; ;
                break;
            case InputEnum.ENTER:
                result = keyboard.IsKeyDown(Keys.Enter) || gamepadPly1.Buttons.Start == ButtonState.Pressed;
                break;
        }

        _previousKeyboardState = keyboard;
        _previousGamePadState = gamepadPly1;

        return result;
    }
}