using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client
{
    public static class InputHandler
    {
        private static readonly List<IKeyboardListener> KeyboardListeners = new List<IKeyboardListener>();
        private static readonly List<IMouseListener> MouseListeners = new List<IMouseListener>();
        private static readonly Dictionary<Keys, InputState> KeyStates = new Dictionary<Keys, InputState>();

        private static readonly Dictionary<MouseButtonType, InputState> MouseStates =
            new Dictionary<MouseButtonType, InputState>();


        internal static void Update()
        {
            DisposeUnusedListeners();

            UpdateKeyboardListeners();
            UpdateMouseListeners();
        }

        private static void DisposeUnusedListeners()
        {
            for (int i = 0; i < KeyboardListeners.Count;)
            {
                if (KeyboardListeners[i].CanDispose())
                    KeyboardListeners.Remove(KeyboardListeners[i]);
                else
                    ++i;
            }
        }

        private static void UpdateKeyboardListeners()
        {
            KeyboardState keyboard = Keyboard.GetState();
            List<Keys> pressedKeys = new List<Keys>(keyboard.GetPressedKeys());
            List<Keys> expiredKeys = new List<Keys>();
            foreach (Keys key in KeyStates.Keys)
            {
                InputState record = KeyStates[key];
                bool currentlyDown = pressedKeys.Contains(key);
                record.ToNextState(currentlyDown);

                if (currentlyDown)
                    pressedKeys.Remove(key);

                FireKeyListeners(key, record);

                if (record.IsExpired())
                    expiredKeys.Add(key);
            }

            // The remaining keys, though triggered, are not mapped yet.
            foreach (Keys key in pressedKeys)
            {
                InputState record = new InputState();
                record.ToNextState(true);
                KeyStates.Add(key, record);
                FireKeyListeners(key, record);
            }

            foreach (Keys key in expiredKeys)
                KeyStates.Remove(key);
        }

        private static void UpdateMouseListeners()
        {
            MouseState mouseState = Mouse.GetState();
            // A total violation of the DRY principle.
            {
                const MouseButtonType buttonType = MouseButtonType.Primary;
                bool currentState = mouseState.LeftButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireMouseListeners(buttonType, MouseStates[buttonType]);                
            }
            
            {
                const MouseButtonType buttonType = MouseButtonType.Middle;
                bool currentState = mouseState.MiddleButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireMouseListeners(buttonType, MouseStates[buttonType]);                
            }

            {
                const MouseButtonType buttonType = MouseButtonType.Secondary;
                bool currentState = mouseState.RightButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireMouseListeners(buttonType, MouseStates[buttonType]);                
            }
        }

        private static void FireKeyListeners(Keys key, InputState record)
        {
            if (record.IsPressed())
                KeyboardListeners.ForEach(listener => listener.KeyPressed(key));
            if (record.IsReleased())
                KeyboardListeners.ForEach(listener => listener.KeyReleased(key));
            if (record.IsHeldDown())
                KeyboardListeners.ForEach(listener => listener.KeyHeldDown(key));
        }

        private static void FireMouseListeners(MouseButtonType buttonType, InputState record)
        {
            if (record.IsPressed())
                MouseListeners.ForEach(listener => listener.ButtonPressed(buttonType));
            if (record.IsReleased())
                MouseListeners.ForEach(listener => listener.ButtonReleased(buttonType));
            if (record.IsHeldDown())
                MouseListeners.ForEach(listener => listener.ButtonHeldDown(buttonType));
        }

        public static void AddMouseListener([NotNull] IMouseListener listener) => MouseListeners.Add(listener);
        public static void RemoveMouseListener([NotNull] IMouseListener listener) => MouseListeners.Remove(listener);

        public static void AddKeyboardListener([NotNull] IKeyboardListener listener) => KeyboardListeners.Add(listener);

        public static void RemoveKeyboardListener([NotNull] IKeyboardListener listener) =>
            KeyboardListeners.Remove(listener);
    }

    internal sealed class InputState
    {
        private const int InactiveExpiryTimeMs = 5 * 60 * 1000;

        private bool _wasDown;
        private bool _isDown;
        private long _lastStateChangeMs;

        public InputState()
        {
            _lastStateChangeMs = new DateTime().Millisecond;
        }

        internal void ToNextState(bool newCurrentState)
        {
            _wasDown = _isDown;
            _isDown = newCurrentState;

            if (_wasDown == !_isDown)
                _lastStateChangeMs = new DateTime().Millisecond;
        }

        internal bool IsHeldDown() => _wasDown && _isDown;
        internal bool IsPressed() => !_wasDown && _isDown;
        internal bool IsReleased() => _wasDown && !_isDown;
        internal bool IsExpired() => new DateTime().Millisecond - _lastStateChangeMs > InactiveExpiryTimeMs;
    }

    public interface IKeyboardListener
    {
        void KeyPressed(Keys key);
        void KeyHeldDown(Keys key);
        void KeyReleased(Keys key);
        bool CanDispose();
    }

    public enum MouseButtonType
    {
        Primary, Middle, Secondary
    }

    public interface IMouseListener
    {
        void ButtonPressed(MouseButtonType mouseButtonType);
        void ButtonHeldDown(MouseButtonType mouseButtonType);
        void ButtonReleased(MouseButtonType mouseButtonType);
        bool CanDispose();
    }
    
}