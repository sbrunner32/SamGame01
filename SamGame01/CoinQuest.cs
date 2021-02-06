using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            Window.Title = "Coins Quest";
            coinsList = new List<CoinSprite>();
            CoinSprite temp = new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
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
            bool addCoin = false;
            foreach (var coin in coinsList)
            {
                ///If the player touches a coin
                if (!coin.Collected && coin.Bounds.CollidesWith(slimeSprite.Bounds))
                {
                    addCoin = true;                    
                    coinsCount++;
                    coin.Collected = true;
                    coin.Warp(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);                    
                }
            }
            ///Adds a coin randomly in the game window
            if(addCoin)
            {
                CoinSprite tempCoin = new CoinSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
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
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
