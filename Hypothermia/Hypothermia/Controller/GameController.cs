using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hypothermia.Controller
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameController : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D texture;


        // temp
        private View.PlayerView playerView;
        private View.GameView view;
        private Model.Player player;

        Texture2D floorTexture;
        Model.GameObject floor;
       

        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Model.BoxCollider floorCollider = new Model.BoxCollider(new Model.GameObject(new Vector2(200, 200), 300, 50));
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

            // TODO: use this.Content to load your game content here

            this.playerView = new View.PlayerView(Content);
            this.texture = Content.Load<Texture2D>("player");
            this.floorTexture = new Texture2D(graphics.GraphicsDevice, 600, 30);
            this.floor = new Model.GameObject(this.floorTexture, new Rectangle(0, 450, this.floorTexture.Width, this.floorTexture.Height));
            this.view = new View.GameView(GraphicsDevice, Content);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(this.player == null)
                this.player = new Model.Player(this.texture);

            
            foreach(Model.Box box in this.view.Boxes) {
                if(this.player.Position.X > box.Rect.Left && this.player.Position.X < box.Rect.Right)
                    if (this.player.DetectCollision(box.Collider))
                    {
                        this.player.OnGround = true;
                    }
                    else
                    {
                        this.player.OnGround = false;
                    }
            }

            /*
            if (this.player.Rect.Intersects(this.floor.Rect)) 
            {
                this.player.OnGround = true;
                this.player.VelocityY = 0;
            }
            else
            {
                this.player.OnGround = false;
            }
             */
             
             
            this.player.Update(elapsedTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Vector2 textureCenterBottomDisplacement = new Vector2 ((float)this.player.Texture.Bounds.Width / 2, (float)this.player.Texture.Bounds.Height);

            spriteBatch.Draw(this.floor.Texture, this.floor.Rect, null, Color.Black);

            spriteBatch.Draw(this.player.Texture, this.player.Position, null, Color.White, 0f, textureCenterBottomDisplacement, 1f, SpriteEffects.None, 0f);
            this.view.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
