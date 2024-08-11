using GameEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Utils
{
    internal class InputUtils
    {
        private KeyboardState _keyboard;
        private GamePadState _gamepadPly1;
        private float _sensibility = Constants.Config.InputSensibility;

        public InputUtils()
        {
            _keyboard = Keyboard.GetState();
            _gamepadPly1 = GamePad.GetState(PlayerIndex.One);
        }

        public bool IsKeyLeft()
        {
            return _keyboard.IsKeyDown(Keys.Left) || _gamepadPly1.ThumbSticks.Left.X < -_sensibility || _gamepadPly1.DPad.Left == ButtonState.Pressed;
        }

        public bool IsKeyRight()
        {
            return _keyboard.IsKeyDown(Keys.Right) || _gamepadPly1.ThumbSticks.Left.X > _sensibility || _gamepadPly1.DPad.Right == ButtonState.Pressed;
        }

        public bool IsKeyUp()
        {
            return _keyboard.IsKeyDown(Keys.Up) || _gamepadPly1.ThumbSticks.Left.Y > _sensibility || _gamepadPly1.DPad.Up == ButtonState.Pressed;
        }

        public bool IsKeyDown()
        {
            return _keyboard.IsKeyDown(Keys.Down) || _gamepadPly1.ThumbSticks.Left.Y < -_sensibility || _gamepadPly1.DPad.Down == ButtonState.Pressed;
        }

        public bool IsKeyEscape()
        {
            return _keyboard.IsKeyDown(Keys.Escape) || _gamepadPly1.Buttons.Back == ButtonState.Pressed;
        }

        public bool IsKeyEnter()
        {
            return _keyboard.IsKeyDown(Keys.Enter) || _gamepadPly1.Buttons.Start == ButtonState.Pressed;
        }
    }
}