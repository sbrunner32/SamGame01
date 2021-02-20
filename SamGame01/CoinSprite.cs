using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SamGame01.Collisions;

namespace SamGame01
{
    /// <summary>
    /// A class to represent coins in game
    /// </summary>
    public class CoinSprite
    {
        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        private Vector2 velocity;

        private float windowWidth;

        private float windowHeight;

        /// <summary>
        /// Bool representing if the coin has been collected or not
        /// </summary>
        public bool Collected { get; set; } = false;

        /// <summary>
        /// Public getter for the position value for the game to use
        /// </summary>
        public Vector2 Position { 
            get
            {
                return position;
            }
            set
            {
                position = value;
                bounds.Center = position - new Vector2(-8, -8);
            }
        }

        /// <summary>
        /// Public getter for the coin's velocity
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Creates a new coin sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public CoinSprite(Vector2 position, float width, float height)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
            velocity = new Vector2(10, 0);
            windowWidth = width;
            windowHeight = height;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("coins");
        }


        ///Code to add the update feature to the CoinSprite itself. Non-functional
        /***
        public void Update(GameTime gameTime)
        {
            bool outOfBounds = false;
            bool addCoin = false;

            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 acceleration = new Vector2(0, 30);
            velocity += acceleration * t;
            position += t * velocity;
            if(outOfBounds)
            {
                this.Warp(windowWidth, windowHeight);
            }
            outOfBounds = true;
        }
        ***/

            /// <summary>
            /// Draws the animated sprite using the supplied SpriteBatch
            /// </summary>
            /// <param name="gameTime">The game time</param>
            /// <param name="spriteBatch">The spritebatch to render with</param>
            public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected) return;
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame > 7) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }

            var source = new Rectangle(animationFrame * 16, 0, 16, 16);
            spriteBatch.Draw(texture, position, source, Color.White);
        }

        /// <summary>
        /// Warps the coin to a random position on the game area and resets it's collected value
        /// </summary>
        public void Warp(float width, float height)
        {
            System.Random rand = new System.Random();
            if (Collected)
            {
                this.position = new Vector2((float)rand.NextDouble() * width, (float)rand.NextDouble() * height);
                this.bounds = new BoundingCircle(position - new Vector2(-8, -8), 8);
                this.Collected = false;
            }
        }
    }
}
