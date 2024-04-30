using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SHY.Classes.Player;
using SHY.Classes.Rendering;
using System;

namespace SHY
{
    public class Game1 : Game
    {
        struct ResizeStatus
        {
            public bool Pending;
            public int Width;
            public int Height;
        }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ResizeStatus _resizing;

        WorldImageDrawObject brick;

        SpriteFont SpriteFont;

        AnimatedWorldDrawObject playerAnimation;

        KeyboardState PreviousState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _resizing = new ResizeStatus(); // Initialize the resizing structure
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        private void OnResize(object sender, EventArgs e)
        {
            _resizing.Pending = true; // Mark resize as pending
            _resizing.Width = Window.ClientBounds.Width;
            _resizing.Height = Window.ClientBounds.Height;
            Camera.RecalculateIdealRatioWindowRectangle(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Camera.CameraRectangle = new(new Point(0,0), GraphicsDevice.Viewport.Bounds.Size);
            Camera.RecalculateIdealRatioWindowRectangle(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);

            Camera.SetCameraCenter(new(0, 0));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            DrawObject.SpriteBatch = _spriteBatch;

            Texture2D bricks = Content.Load<Texture2D>("MarbleBrickTile");
            brick = new(bricks, new(-80, -80, 160, 160));

            SpriteFont = Content.Load<SpriteFont>("Font");

            // TODO: use this.Content to load your game content here

            playerAnimation = new(Content.Load<Texture2D>("PlaceholderPlayer"), new(-80, -80, 160, 160),"../../../Classes/Rendering/Animations/PlayerAnimation.json");
            playerAnimation.manager.SetCurrentAnimation("idle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.W)) Player.Position.Y -= 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.S)) Player.Position.Y += 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.A)) Player.Position.X -= 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.D)) Player.Position.X += 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.W)) DrawingTransform.YOff -= 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.S)) DrawingTransform.YOff += 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.A)) DrawingTransform.XOff -= 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.D)) DrawingTransform.XOff += 1;
            //if (Keyboard.GetState().IsKeyDown(Keys.Q)) DrawingTransform.Scale -= 0.01f;
            //if (Keyboard.GetState().IsKeyDown(Keys.E)) DrawingTransform.Scale += 0.01f;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space) !& PreviousState.IsKeyDown(Keys.Space)) playerAnimation.manager.SetCurrentAnimation("attack");
            if (Keyboard.GetState().IsKeyDown(Keys.D) !& PreviousState.IsKeyDown(Keys.D)) playerAnimation.manager.SetCurrentAnimation("walk");
            if (Keyboard.GetState().IsKeyDown(Keys.W) !& PreviousState.IsKeyDown(Keys.W)) playerAnimation.manager.SetCurrentAnimation("idle");

            playerAnimation.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

            // TODO: Add your update logic here

            base.Update(gameTime);

            PreviousState = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);

            //_spriteBatch.DrawString(SpriteFont, playerAnimation.CurrentFrame().ToString(), new(10,10), Color.AliceBlue);

            //brick.Draw(0,0);

            playerAnimation.Draw(0,0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}