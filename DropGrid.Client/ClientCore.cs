#region Using Statements
using System;
using System.Collections.Generic;
using DropGrid.Client.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DropGrid.Client
{
    
    public class GameEngine : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<StateId, GameState> _gameStates;
        private GameState _currentState;
        private GraphicsRenderer _renderer;

        public GameEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _gameStates = new Dictionary<StateId, GameState>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            RegisterGameState(new InitialiseState());
            RegisterGameState(new MenuState());
            RegisterGameState(new GameplayState());
            EnterState(StateId.Initialise);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderer = new GraphicsRenderer(_graphics, GraphicsDevice, _spriteBatch);

            //TODO: use this.Content to load your game content here 
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
            _currentState.Update(this, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _currentState.Draw(this, _renderer, gameTime);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a game state instance to the game state map.
        /// </summary>
        /// <param name="id">The state identifier.</param>
        /// <param name="state">The state instance.</param>
        private void RegisterGameState(GameState state)
        {
            if (state == null)
                throw new ArgumentException("Cannot register a null state!");
            StateId id = state.GetId();
            _gameStates.Add(id, state);
        }

        public void EnterState(StateId id)
        {
            GameState state = _gameStates[id];
            if (state == null)
                throw new InvalidOperationException("Attempting to switch to state id '" + state.ToString() + "' which has a null state instance!");
            if (_currentState != null)
                _currentState.onExit();
            _currentState = state;
            _currentState.onEnter();
        }
    }

    class GraphicsRenderer
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public GraphicsRenderer(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this._graphicsDeviceManager = graphicsDeviceManager;
            this._graphicsDevice = graphicsDevice;
            this._spriteBatch = spriteBatch;
        }

        private Texture2D CreateSampleTexture(GraphicsDevice graphics, Color color)
        {
            Texture2D texture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { color });
            return texture;
        }

        public void DrawFilledRectangle(double x, double y, double width, double height, Color fillColor)
        {
            Texture2D texture = CreateSampleTexture(_graphicsDevice, fillColor);
            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, new Rectangle((int) x, (int) y, (int) width, (int) height), fillColor);
            _spriteBatch.End();
        }
    }

    namespace State
    {
        abstract class GameState
        {
            public const int INITIALISE = 0;
            public const int MENU = 1;
            public const int GAMEPLAY = 2;

            public abstract StateId GetId();

            public abstract void Draw(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime);
            public abstract void Update(GameEngine engine, GameTime gameTime);

            public void onEnter() {}
            public void onExit() {}
        }

        /// <summary>
        /// 
        /// </summary>
        public enum StateId
        {
            Initialise,
            Menu,
            Gameplay
        }

        class InitialiseState : GameState
        {
            public override StateId GetId() => StateId.Initialise;

            public override void Draw(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
            {

            }

            public override void Update(GameEngine engine, GameTime gameTime)
            {
                if (IsInitializationFinished())
                    engine.EnterState(StateId.Gameplay);
            }

            private bool IsInitializationFinished()
            {
                // TODO: Implement deferred resource loading later.
                return true;
            }
        }

        class MenuState : GameState
        {
            public override StateId GetId() => StateId.Menu;

            public override void Draw(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime) => throw new NotImplementedException();

            public override void Update(GameEngine engine, GameTime gameTime) => throw new NotImplementedException();
        }

        class GameplayState : GameState
        {
            public override StateId GetId() => StateId.Gameplay;

            public override void Draw(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
            {
                renderer.DrawFilledRectangle(20, 20, 200, 200, Color.White);
            }

            public override void Update(GameEngine engine, GameTime gameTime)
            {
            }
        }
    }
}
