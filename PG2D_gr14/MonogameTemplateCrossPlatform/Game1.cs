using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace MonogameTemplateCrossPlatform
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 position;
        Texture2D character;

        List<Vector2> bullets = new List<Vector2>();
        List<Vector2> bulletsToRemove = new List<Vector2>();
        Texture2D bullet;

        private static readonly System.TimeSpan intervalBetweenAttack = System.TimeSpan.FromMilliseconds(250);
        private System.TimeSpan lastTimeAttack;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            character = Content.Load<Texture2D>("images/character");
            bullet = Content.Load<Texture2D>("images/Bullet");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            character.Dispose();
            character = null;
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            /* // Poll for current keyboard state
             KeyboardState state = Keyboard.GetState();

             // If they hit esc, exit
             if (state.IsKeyDown(Keys.Escape))
                 Exit();

             // Move our sprite based on arrow keys being pressed:
             if (state.IsKeyDown(Keys.Right))
                 position.X += 10;
             if (state.IsKeyDown(Keys.Left))
                 position.X -= 10;
             if (state.IsKeyDown(Keys.Up))
                 position.Y -= 10;
             if (state.IsKeyDown(Keys.Down))
                 position.Y += 10;

             GraphicsDevice.Clear(Color.CornflowerBlue);

             spriteBatch.Begin();

             spriteBatch.Draw(character, new Rectangle(position, 350, 100, 70), Color.White);

             spriteBatch.End();

             base.Update(gameTime);
             // TODO: Add your update logic here*/


            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                position.X += 10;
                if (position.X > graphics.PreferredBackBufferWidth - 100)
                    position.X = graphics.PreferredBackBufferWidth - 100;

            }

            if (state.IsKeyDown(Keys.Left))
            {
                position.X -= 10;
                if (position.X < 0)
                    position.X = 0;
            }

            if (state.IsKeyDown(Keys.Up))
            {
                position.Y -= 10;
                if (position.Y < 0)
                    position.Y = 0;
            }

            if (state.IsKeyDown(Keys.Down))
            {
                position.Y += 10;
                if (position.Y > graphics.PreferredBackBufferHeight - 70)
                    position.Y = graphics.PreferredBackBufferHeight - 70;

            }

            if (state.IsKeyDown(Keys.Space))
            {
                if (lastTimeAttack + intervalBetweenAttack < gameTime.TotalGameTime)
                {
                    if (bullets.Count < 10)
                    {
                        bullets.Add(new Vector2(position.X + 90, position.Y + 48));
                    }
                    lastTimeAttack = gameTime.TotalGameTime;
                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if( bullets.Count > 0 )
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    Vector2 currentBullet = bullets[i];
                    if (bullets[i].X > 1000)
                        bulletsToRemove.Add(bullets[i]);
                    else
                    {
                        currentBullet.X += 20;
                        bullets.RemoveAt(i);
                        bullets.Insert(i, currentBullet);
                        spriteBatch.Draw(bullet, new Rectangle((int)bullets[i].X, (int)bullets[i].Y, 20, 10), Color.White);
                    }
                }

                for (int i = 0; i < bulletsToRemove.Count; i++)
                    bullets.Remove(bulletsToRemove[i]);
                bulletsToRemove.Clear();
                
            }
            spriteBatch.Draw(character, new Rectangle( (int) position.X, (int) position.Y, 100, 70), Color.White);

    
            spriteBatch.End();
            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
