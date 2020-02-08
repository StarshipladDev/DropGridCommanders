using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropGrid.Client
{
    /// <summary>
    /// Central input handling mechanism for the game engine.<br />
    /// <br />
    /// Always prefer using listeners to polling direct mouse and keyboard states from MonoGame pipelines.<br />
    /// This is because <c cref="InputHandler">InputHandler</c> is able to distinguish a mouse/key press from a hold.
    /// </summary>
    public static class InputHandler
    {
        private static readonly List<IKeyboardListener> KeyboardListeners = new List<IKeyboardListener>();
        private static readonly List<IMouseListener> MouseListeners = new List<IMouseListener>();
        
        private static readonly Dictionary<Keys, InputState> KeyStates = new Dictionary<Keys, InputState>();
        private static readonly Dictionary<MouseButtonType, InputState> MouseStates = new Dictionary<MouseButtonType, InputState>();

        /// <summary>
        /// Updates the internal mouse/keyboard state on engine update.<br />
        /// <br />
        /// This is invoked automatically by the <c>GameEngine</c> on each update cycle.<br />
        /// You should not, under any other circumstance, invoke this method anywhere else. 
        ///
        /// <seealso cref="GameEngine.Update(GameTime)"/>
        /// </summary>
        internal static void Update()
        {
            DisposeUnusedListeners();

            UpdateKeyboardStates();
            UpdateMouseStates();
        }

        /// <summary>
        /// Removes all unused listeners --listeners that are self-described as disposable-- from the listener list.
        ///
        /// <seealso cref="IKeyboardListener.CanDispose()"/>
        /// <seealso cref="IMouseListener.CanDispose()" /> 
        /// </summary>
        private static void DisposeUnusedListeners()
        {
            for (int i = 0; i < KeyboardListeners.Count;)
            {
                if (KeyboardListeners[i].CanDispose())
                    RemoveKeyboardListener(KeyboardListeners[i]);
                else
                    ++i;
            }
            
            for (int i = 0; i < MouseListeners.Count;)
            {
                if (MouseListeners[i].CanDispose())
                    RemoveMouseListener(MouseListeners[i]);
                else
                    ++i;
            }
        }

        private static void UpdateKeyboardStates()
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

                FireTriggeredKeyListenerEvents(key, record);

                if (record.IsExpired())
                    expiredKeys.Add(key);
            }

            // The remaining keys, though triggered, are not mapped yet.
            // in which case we will update them, and then add them to the dictionary
            // so that they can be polled starting from the next update cycle.
            foreach (Keys key in pressedKeys)
            {
                InputState record = new InputState();
                record.ToNextState(true);
                KeyStates.Add(key, record);
                
                FireTriggeredKeyListenerEvents(key, record);
            }

            foreach (Keys key in expiredKeys)
                KeyStates.Remove(key);
        }

        private static void UpdateMouseStates()
        {
            MouseState mouseState = Mouse.GetState();
            // A total violation of the DRY principle.
            {
                // Update state for the left mouse button
                const MouseButtonType buttonType = MouseButtonType.Primary;
                bool currentState = mouseState.LeftButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireTriggeredMouseListenerEvents(buttonType, MouseStates[buttonType]);                
            }
            
            {
                // Update state for the middle mouse button
                const MouseButtonType buttonType = MouseButtonType.Middle;
                bool currentState = mouseState.MiddleButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireTriggeredMouseListenerEvents(buttonType, MouseStates[buttonType]);                
            }

            {
                // Update state for the right mouse button
                const MouseButtonType buttonType = MouseButtonType.Secondary;
                bool currentState = mouseState.RightButton == ButtonState.Pressed;
                if (!MouseStates.ContainsKey(buttonType))
                    MouseStates.Add(buttonType, new InputState());
                MouseStates[buttonType].ToNextState(currentState);
                FireTriggeredMouseListenerEvents(buttonType, MouseStates[buttonType]);                
            }
            
            // TODO: State updates for mouse scroll wheel, mouse motion etc.
        }

        /// <summary>
        /// If there is any change in state between the current poll and the last poll result, broadcast the event<br />
        /// to the corresponding <c cref="IKeyboardListener">IKeyboardListener</c>.
        /// </summary>
        /// <param name="key">The key that is affected.</param>
        /// <param name="record">The state record for the key.</param>
        private static void FireTriggeredKeyListenerEvents(Keys key, InputState record)
        {
            if (record.IsPressed())
                KeyboardListeners.ForEach(listener => listener.KeyPressed(key));
            if (record.IsReleased())
                KeyboardListeners.ForEach(listener => listener.KeyReleased(key));
            if (record.IsHeldDown())
                KeyboardListeners.ForEach(listener => listener.KeyHeldDown(key));
        }

        /// <summary>
        /// If there is any change in state between the current poll and the last poll result, broadcast the event<br />
        /// to the corresponding <c cref="IMouseListener">IMouseListener</c>.
        /// </summary>
        /// <param name="buttonType"></param>
        /// <param name="record"></param>
        private static void FireTriggeredMouseListenerEvents(MouseButtonType buttonType, InputState record)
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

    /// <summary>
    /// Represents the past two states of a given input item. The combination of which determines its current status.<br />
    /// </summary>
    internal sealed class InputState
    {
        // The amount of time, in ms, to actively poll for this input item.
        // If the state is not updated for longer than this time, consider it a rarely
        // pressed button, and its existence in the state dictionary is probably 
        // a waste of resources.
        private const int InactiveExpiryTimeMs = 5 * 60 * 1000;
        
        // Last time there was a state change for this input object. 
        private long _lastStateChangeMs;

        private bool _wasDown;
        private bool _isDown;

        public InputState()
        {
            _lastStateChangeMs = new DateTime().Millisecond;
        }

        /// <summary>
        /// Updates the internal states for the next cycle. 
        /// </summary>
        /// <param name="newCurrentState">true if the new current state is <i>active</i>, false otherwise.</param>
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

    /// <summary>
    /// Registers event responders to engine keyboard input.  
    /// </summary>
    public interface IKeyboardListener
    {
        /// <summary>
        /// Invoked when the key state changes from unpressed to pressed. It is only called once during this state change.<br />
        /// If the key is continuously held down, <c>KeyHeldDown(Key)</c> is invoked instead.
        /// </summary>
        /// <see cref="Keys"/>
        /// <param name="key">The key that is pressed.</param>
        void KeyPressed(Keys key);
        
        /// <summary>
        /// Invoked when the key state remains held down for more than 1 update cycle. As long as this is true, this method<br />
        /// will be called continuously.
        /// </summary>
        /// <see cref="Keys"/>
        /// <param name="key">The key that is held down.</param>
        void KeyHeldDown(Keys key);
        
        /// <summary>
        /// Invoked when the key state changes from pressed to unpressed. It is only called once during this state change.<br />
        /// </summary>
        /// <see cref="Keys"/>
        /// <param name="key">The key that has just been released.</param>
        void KeyReleased(Keys key);
        
        /// <summary>
        /// Used by the <c>InputHandler</c> to determine whether to remove this listener from its list of active listeners.
        /// </summary>
        /// <see cref="Keys"/>
        /// <returns>true of the listener is no longer active and should be removed, false otherwise.</returns>
        bool CanDispose();
    }

    /// <summary>
    /// Represents the different types of mouse button supported by the game engine.
    /// </summary>
    public enum MouseButtonType
    {
        Primary, Middle, Secondary
    }

    public interface IMouseListener
    {
        void ButtonPressed(MouseButtonType mouseButtonType);
        
        /// <summary>
        /// Invoked when the mouse button state changes from unpressed to pressed. It is only called once during this state change.<br />
        /// </summary>
        /// <see cref="MouseButtonType"/>
        /// <param name="mouseButtonType">The mouse button that is pressed.</param>
        void ButtonHeldDown(MouseButtonType mouseButtonType);
        
        /// <summary>
        /// Invoked when the mouse button state remains held down. It is called continuously as long as the button remains held.<br />
        /// </summary>
        /// <see cref="MouseButtonType"/>
        /// <param name="mouseButtonType">The mouse button that has been held down.</param>
        void ButtonReleased(MouseButtonType mouseButtonType);
        
        /// <summary>
        /// Used by the <c>InputHandler</c> to determine whether to remove this listener from its list of active listeners.
        /// </summary>
        /// <returns>true of the listener is no longer active and should be removed, false otherwise.</returns>
        bool CanDispose();
    }
    
}