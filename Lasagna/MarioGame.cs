using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Sprint0
{
    public class MarioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;
        private int screenWidth, screenHeight;
        private KeyboardController keyControl;
        private NoMoveAndNoAnimationSprite noMoveAndNoAnimSprite;
        private NoMoveAndAnimationSprite noMoveAndAnimSprite;
        private MoveAndNoAnimationSprite moveAndNoAnimSprite;
        private MoveAndAnimationSprite moveAndAnimSprite;
        private ISprite currentSprite;

        public MarioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            gameTime = new GameTime();
            keyControl = new KeyboardController();
            noMoveAndNoAnimSprite = new NoMoveAndNoAnimationSprite();
            noMoveAndAnimSprite = new NoMoveAndAnimationSprite();
            moveAndNoAnimSprite = new MoveAndNoAnimationSprite();
            moveAndAnimSprite = new MoveAndAnimationSprite();

            //Subscribe to events
            MarioEvents.OnQuit += OnQuit;
            
            //Set our current sprite initially to stationary sprite
            currentSprite = noMoveAndNoAnimSprite;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            noMoveAndNoAnimSprite.LoadContent(this);
            noMoveAndAnimSprite.LoadContent(this);
            moveAndNoAnimSprite.LoadContent(this);
            moveAndAnimSprite.LoadContent(this);
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();
            currentSprite.Update(gameTime, screenWidth, screenHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentSprite.Draw(spriteBatch, screenWidth, screenHeight);

            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit()
        {
            Exit();
        }

        private void OnSelectNoMoveAndNoAnimation()
        {
            ChangeCurrentSprite(SpriteType.NoMoveAndNoAnimation);
        }

        private void OnSelectNoMoveAndAnimation()
        {
            ChangeCurrentSprite(SpriteType.NoMoveAndAnimation);
        }

        private void OnSelectMoveAndNoAnimation()
        {
            ChangeCurrentSprite(SpriteType.MoveAndNoAnimation);
        }

        private void OnSelectMoveAndAnimation()
        {
            ChangeCurrentSprite(SpriteType.MoveAndAnimation);
        }

        private void ChangeCurrentSprite(SpriteType newSpriteType)
        {
            ISprite oldSprite = currentSprite;

            switch (newSpriteType)
            {
                case SpriteType.NoMoveAndNoAnimation:
                    currentSprite = noMoveAndNoAnimSprite;
                    break;

                case SpriteType.NoMoveAndAnimation:
                    currentSprite = noMoveAndAnimSprite;
                    break;

                case SpriteType.MoveAndNoAnimation:
                    currentSprite = moveAndNoAnimSprite;
                    break;

                case SpriteType.MoveAndAnimation:
                    currentSprite = moveAndAnimSprite;
                    break;

                default:
                    Debug.WriteLine("Invalid sprite type passed to ChangeCurrentSprite.");
                    break;
            }

            //If sprite was changed, reset current sprite. This resets position and animation
            if (oldSprite != currentSprite)
                currentSprite.ResetSprite(screenWidth, screenHeight);
        }
    }
}
