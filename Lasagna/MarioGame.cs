﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lasagna
{
    public class MarioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;
        private KeyboardController keyControl;

        ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
        ISprite marioStanding;
        ISprite marioRunning;
        ISprite currentSprite;

        public MarioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            gameTime = new GameTime();
            keyControl = new KeyboardController();

            //Subscribe to events
            MarioEvents.OnQuit += OnQuit;
            MarioEvents.OnMoveRight += OnMoveRight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            EnemySpriteFactory.Instance.LoadAllContent(Content);
            ItemSpriteFactory.Instance.LoadAllContent(Content);
            MarioSpriteFactory.Instance.LoadAllContent(Content);
            ProjectileSpriteFactory.Instance.LoadAllContent(Content);
            TileSpriteFactory.Instance.LoadAllContent(Content);

            ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
            marioStanding = MarioSpriteFactory.Instance.CreateSprite_MarioStanding();
            marioRunning = MarioSpriteFactory.Instance.CreateSprite_MarioRunning();

            currentSprite = marioStanding;
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();

            ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
            currentSprite.Update(gameTime, 32, 32);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
            currentSprite.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit()
        {
            Exit();
        }

        ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
        private void OnMoveRight ()
        {
            if (currentSprite == marioRunning)
                currentSprite = marioStanding;
            else
                currentSprite = marioRunning;
        }
    }
}
