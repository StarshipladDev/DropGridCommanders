using DropGridCommanderVersion2.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DropGridCommanderVersion2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        static Texture2D GroundTile;
        static Texture2D GroundTile1;
        static Texture2D GroundTileForest;
        static Texture2D selectionBorder;
        static Texture2D[,] arrayTerrain = new Texture2D[10, 10];
        private bool wPressed = false;
        System.Drawing.Point selectionPoint = new System.Drawing.Point(0, 0);
        Unit unitman; 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            //Setup Array
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                for (int f = 0; f < 10; f++)
                {
                    Vector2 topLeftOfSprite = new Vector2(40 * i, 40 * f);
                    Color tintColor = Color.White;
                    if (rand.Next(4) > 2)
                    {

                        arrayTerrain[i, f] = GroundTile;
                    }
                    else if (rand.Next(4) > 1)
                    {

                        arrayTerrain[i, f] = GroundTile1;
                    }
                    else
                    {

                        arrayTerrain[i, f] = GroundTileForest;
                    }
                }
            }

            //Finish SetupArray

            //SetHeight/Width
            graphics.PreferredBackBufferWidth = 400;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 400;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            String[] weapons = new string[] { "Bullpup","Sniper","Shotgun","Machinegun"};
            unitman = new Unit(rand.Next(10), rand.Next(10) ,weapons[rand.Next(weapons.Length)], this.GraphicsDevice);
            //End set height/width
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            System.IO.Stream stream;
            stream = TitleContainer.OpenStream("Content/Ground_City.png");

            GroundTile = Texture2D.FromStream(this.GraphicsDevice, stream);
            stream.Dispose();
            stream= TitleContainer.OpenStream("Content/Border.png");
            selectionBorder = Texture2D.FromStream(this.GraphicsDevice, stream);
            stream.Dispose();
            stream = TitleContainer.OpenStream("Content/Ground_City2.png");

            GroundTile1 = Texture2D.FromStream(this.GraphicsDevice, stream);
            stream.Dispose();
            stream = TitleContainer.OpenStream("Content/Ground_ForestTree.png");

            GroundTileForest = Texture2D.FromStream(this.GraphicsDevice, stream);
            stream.Dispose();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState state = Keyboard.GetState();
            if(state.IsKeyDown(Keys.W) && wPressed == false)
            {
                wPressed = true;
                Random rand = new Random();
                unitman= new Unit(rand.Next(10), rand.Next(10),"Bullpup",this.GraphicsDevice);
            }
            else if (state.IsKeyUp(Keys.W) && wPressed == true)
            {
                wPressed = false;

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Random rand = new Random();
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            for(int i=0; i< 10; i++)
            {
                for(int f=0; f < 10; f++)
                {
                    Vector2 topLeftOfSprite = new Vector2(40*i,40*f);
                    Color tintColor = Color.White;
                    _spriteBatch.Draw(arrayTerrain[i,f], topLeftOfSprite, tintColor);
                }
            }
            Vector2 v= new Vector2(selectionPoint.X, selectionPoint.Y);
            Color color = Color.White;
            _spriteBatch.Draw(selectionBorder, v, color);
            unitman.Draw(_spriteBatch,ref arrayTerrain);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
