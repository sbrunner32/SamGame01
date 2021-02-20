using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SamGame01
{
    /// <summary>
    /// Class for the game rainbow six siege
    /// </summary>
    public class CoinQuest : Game
    {


        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<CoinSprite> coinsList;
        private SlimeSprite slimeSprite;
        private SpriteFont spriteFont;
        private int coinsCount;

        private int width;
        private int height;

        private SoundEffect coinPickup;
        private string coinSoundName = "ICanTellWav";


        /// <summary>
        /// Constructor for our game 
        /// </summary>
        public CoinQuest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game 
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            System.Random rand = new System.Random();
            Window.Title = "Coins Quest 2";
            coinsList = new List<CoinSprite>();
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;
            CoinSprite temp = new CoinSprite(new Vector2((float)rand.NextDouble() * width, (float)rand.NextDouble() * height), width, height);
            coinsList.Add(temp);           
          
            coinsCount = 0;
            slimeSprite = new SlimeSprite();

            base.Initialize();
        }

        /// <summary>
        /// Loads content for the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (var coin in coinsList) coin.LoadContent(Content);
            slimeSprite.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("arial");
            coinPickup = Content.Load<SoundEffect>(coinSoundName);
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            System.Random rand = new System.Random();
            // TODO: Add your update logic here
            slimeSprite.Update(gameTime);
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 acceleration = new Vector2(0, 30);
            bool addCoin = false;
            foreach (var coin in coinsList)
            {
                ///If the player touches a coin
                if (!coin.Collected && coin.Bounds.CollidesWith(slimeSprite.Bounds))
                {
                    addCoin = true;                    
                    coinsCount++;
                    coin.Collected = true;
                    coin.Warp(width, height);
                    coinPickup.Play();
                }
                /// If the coin goes beneath the game window
                if (coin.Position.Y > height)
                {
                    Vector2 temp = new Vector2(coin.Position.X, 0);
                    coin.Position = temp;
                }
                /// If the coin goes past the right boundary
                if (coin.Position.X > width)
                {
                    Vector2 temp = new Vector2(0, coin.Position.Y);
                    coin.Position = temp;
                }
                coin.Velocity += acceleration * t;
                coin.Position += t * coin.Velocity;
                /// Check to see if the player has gotten enough coins to win 
                if(coinsCount> 9)
                {
                    this.Exit();
                }
            }
            ///Adds a coin randomly in the game window
            if(addCoin)
            {
                CoinSprite tempCoin = new CoinSprite(
                    new Vector2((float)rand.NextDouble() * width, 
                    (float)rand.NextDouble() * height), 
                    width, 
                    height);
                tempCoin.LoadContent(Content);
                coinsList.Add(tempCoin);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (var coin in coinsList)
            {
                coin.Draw(gameTime, spriteBatch);

            }

            slimeSprite.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(spriteFont, $"Coin Count: {coinsCount}", new Vector2(2, 2), Color.Gold);
            if(coinsCount< 4)
            {
                spriteBatch.DrawString(spriteFont, $"Collect 10 Coins to Win", new Vector2(420, 2), Color.Gold);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
