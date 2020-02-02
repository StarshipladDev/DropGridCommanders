#region Using Statements
using System;
using System.Collections.Generic;
using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using DropGrid.Client.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DropGrid.Client
{
    /// <summary>
    /// This is the main client engine. 
    /// It is responsible for initializing, drawing and updating the game state.
    /// </summary>
    public class GameEngine : Game
    {
        // The enlargement factor for game art
        public static readonly int GRAPHICS_SCALE = 3;

        // For drawing objects.
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GraphicsRenderer Renderer { get; private set; }

        // For game state management.
        private readonly Dictionary<StateId, EngineState> _gameStates;
        private EngineState _currentState;

        /// <summary>
        /// Sets up internal objects to manage the game loop.
        /// Do not load game assets here.
        /// </summary>
        public GameEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            AssetLoader.Initialise(this);
            _gameStates = new Dictionary<StateId, EngineState>();
        }

        /// <summary>
        /// Sets up the game states and other core elements of the game loop.
        /// Asset loading is done in InitialisationState.
        /// 
        /// Do not load assets here.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            RegisterGameState(new LoadingState());
            RegisterGameState(new MenuState());
            RegisterGameState(new GameplayState());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Renderer = new GraphicsRenderer(_spriteBatch, ViewPerspectives.ISOMETRIC);
            EnterState(StateId.Initialise);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            if (_currentState.Initialised)
                _currentState.Update(this, gameTime);
            else
                _currentState.Initialise(this);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (_currentState.Initialised)
                _currentState.Render(this, Renderer, gameTime);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a game state instance to the game state map.
        /// </summary>
        /// <param name="state">The state instance.</param>
        private void RegisterGameState(EngineState state)
        {
            if (state == null)
                throw new ArgumentException("Cannot register a null state!");
            StateId id = state.GetId();
            _gameStates.Add(id, state);
        }

        /// <summary>
        /// Transition into the specified state.
        /// </summary>
        /// <param name="id">StateId of the new state.</param>
        public void EnterState(StateId id)
        {
            EngineState state = _gameStates[id];
            if (state == null)
                throw new InvalidOperationException("Attempting to switch to state id '" + id + "' which has a null state instance!");
            if (_currentState != null)
                _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
    }
}
