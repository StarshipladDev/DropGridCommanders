#region Using Statements
using System;
using System.Collections.Generic;
using DropGrid.Client.Asset;
using DropGrid.Client.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace DropGrid.Client
{
    /// <summary>
    /// This is the main client engine code. 
    /// It is responsible for initializing, drawing and updating the game state.
    /// </summary>
    public class GameEngine : Game
    {
        // For drawing objects.
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        // For game state management.
        private Dictionary<StateId, GameState> _gameStates;
        private GameState _currentState;
        
        /// <summary>
        /// Sets up internal objects to manage the game loop.
        /// Do not load game assets here.
        /// </summary>
        public GameEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            AssetLoader.Initialise(this);
            _gameStates = new Dictionary<StateId, GameState>();
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
            _currentState.Draw(this, _spriteBatch, gameTime);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a game state instance to the game state map.
        /// </summary>
        /// <param name="state">The state instance.</param>
        private void RegisterGameState(GameState state)
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
            GameState state = _gameStates[id];
            if (state == null)
                throw new InvalidOperationException("Attempting to switch to state id '" + state.ToString() + "' which has a null state instance!");
            if (_currentState != null)
                _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }
    }

    namespace State
    {
        /// <summary>
        /// Each GameState handles one succinct set of game routines. They each have a unique identifiable state ID.
        /// </summary>
        abstract class GameState
        {
            public abstract StateId GetId();

            public abstract void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime);
            
            public abstract void Update(GameEngine engine, GameTime gameTime);

            public void OnEnter() { }
            
            public void OnExit() { }
        }

        /// <summary>
        /// A list of supported states by the game client.
        /// </summary>
        public enum StateId
        {
            Initialise,
            Menu,
            Gameplay
        }

        /// <summary>
        /// The initialisation state handles deferred content loading. It is too costly to load ALL game assets into memory during startup.
        /// When new unloaded assets have been requested, we switch to this state and load them.
        /// </summary>
        class LoadingState : GameState
        {
            public override StateId GetId() => StateId.Initialise;

            public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
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

        /// <summary>
        /// Handles main menu logic.
        /// </summary>
        class MenuState : GameState
        {
            public override StateId GetId() => StateId.Menu;

            public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime) => throw new NotImplementedException();

            public override void Update(GameEngine engine, GameTime gameTime) => throw new NotImplementedException();
        }

        /// <summary>
        /// The crux of the client-side game logic belongs here.
        /// </summary>
        class GameplayState : GameState
        {
            public override StateId GetId() => StateId.Gameplay;

            // TODO: This is only temporary
            public int turn;
            bool loaded = false;
            AssetsToUse assetsToUse;
            public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
            {
                if (!loaded)
                {
                    assetsToUse = new AssetsToUse().Add(new Spritesheet("basic_ground_tiles", 128));

                    AssetLoader.LoadQueue.Add(assetsToUse);
                    AssetLoader.LoadQueue.LoadAll();
                    loaded = true;
                    turn = 0;
                }
                spriteBatch.Begin();
                spriteBatch.Draw((Texture2D) ((Spritesheet) assetsToUse.GetAsset("basic_ground_tiles")).getSpriteAt(0, 0).GetData(), new Vector2(200, 200));
                spriteBatch.End();
            }

            public override void Update(GameEngine engine, GameTime gameTime)
            {
                
            }

            
        }
        ///<summary>
        ///The superclass for 'displayed' objects on the game other than the battle map <p>
        /// Breaks down into <see cref="AbstractEntity"/> and <see cref="RealEntity"/>
        ///</summary>
        abstract class Entity
        {
            private int gridX;
            private int gridY;
            private String name;
            private Texture2D attatchedSprite;
            public void SetEntity(int gridX, int gridY, String name)
            {
                this.gridX = gridX;
                this.gridY = gridY;
                this.name = name;
                this.attatchedSprite = null;
            }
            public void SetTexture(Sprite place)
            {
                this.attatchedSprite = (Texture2D) place.GetData();
            }
            public int getGridX()
            {
                return this.gridX;
            }
            public int getGridY()
            {
                return this.gridY;
            }
        }
        abstract class RealEntity : Entity
        {
            private bool interactable;
            public void SetRealEntity(int gridX, int gridY, String name, bool interactable)
            {
                SetEntity(gridX,gridY,name);
                this.interactable = interactable;
            }
            public bool getInteractable()
            {
                return interactable;
            }
        }
        abstract class AbstractEntity : Entity
        {
            int turnPlaced;
            public void SetAbstractEntity(int gridX, int gridY, String name, int turnPlaced)
            {
                SetEntity(gridX, gridY, name);
                this.turnPlaced = turnPlaced;
            }
            public int getturnPlaced()
            {
                return turnPlaced;
            }
        }
        class Unit : RealEntity
        {
            private int health, range, sniperRange, damageNorm, damageBuilding, cost,damageAssault;
            public Unit(String unitType,int gridX,int gridY)
            {
                
                switch (unitType)
                {
                    case ("Infantry"):
                        this.health = 4;
                        this.range = 1;
                        this.sniperRange = 0;
                        this.damageNorm = 2;
                        this.damageBuilding = 1;
                        this.damageAssault = 2;
                        this.cost = 1;
                        break;
                    default:
                        this.health = 0;
                        this.range = 0;
                        this.sniperRange = 0;
                        this.damageNorm = 0;
                        this.damageBuilding = 0;
                        this.cost = 0;
                        this.damageAssault = 0;
                        break;
                }
                SetRealEntity(gridX, gridY, unitType, true);
            }
        }
        class Terrain : RealEntity
        {
            public Terrain(String name, int gridX, int gridY)
            {
                SetRealEntity(gridX, gridY, name, false);
            }
        }
        class DropOrder : AbstractEntity
        {
            public DropOrder(String name, int gridX, int gridY, GameplayState gs)
            {
                SetAbstractEntity(gridX, gridY, name,gs.turn);
            }
        }
        class AbilityOrder : AbstractEntity
        {
            public AbilityOrder(String name, int gridX, int gridY, GameplayState gs)
            {
                SetAbstractEntity(gridX, gridY, name, gs.turn);
            }
        }
        class MoveOrder : AbstractEntity
        {
            public MoveOrder(String name, int gridX, int gridY, GameplayState gs)
            {
                SetAbstractEntity(gridX, gridY, name, gs.turn);
            }
        }
    }
}
