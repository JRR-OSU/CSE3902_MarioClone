using Microsoft.Xna.Framework;
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

        ISprite breakableBrick;
        ISprite floor;
        ISprite itemBlockUsed;
        ISprite pipeBase;
        ISprite pipeTip;
        ISprite questionBlock;
        ISprite unbreakableBlock;
        ISprite flag;
        ISprite flagPole;

        ISprite fireballDefault;
        ISprite fireballExplode;

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

            breakableBrick = TileSpriteFactory.Instance.CreateSprite_BreakableBrick();
            floor = TileSpriteFactory.Instance.CreateSprite_Floor();
            itemBlockUsed = TileSpriteFactory.Instance.CreateSprite_ItemBlockUsed() ;
            pipeBase = TileSpriteFactory.Instance.CreateSprite_Pipe_Base();
            pipeTip = TileSpriteFactory.Instance.CreateSprite_Pipe_Tip();
            questionBlock = TileSpriteFactory.Instance.CreateSprite_QuestionBlock();
            unbreakableBlock = TileSpriteFactory.Instance.CreateSprite_UnbreakableBlock();
            flag = TileSpriteFactory.Instance.CreateSprite_Flag();
            flagPole = TileSpriteFactory.Instance.CreateSprite_FlagPole();

            fireballDefault = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Default();
            fireballExplode = ProjectileSpriteFactory.Instance.CreateSprite_Fireball_Explode();
            currentSprite = marioStanding;
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();

            ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
            currentSprite.Update(gameTime, 300, 300);

            unbreakableBlock.Update(gameTime, 70, 200);
            itemBlockUsed.Update(gameTime, 140, 200);
            questionBlock.Update(gameTime, 210, 200);
            breakableBrick.Update(gameTime, 280, 200);
            floor.Update(gameTime, 350, 200);
            pipeTip.Update(gameTime, 420, 200);
            pipeBase.Update(gameTime, 520, 200);

            flag.Update(gameTime, 610, 200);
            flagPole.Update(gameTime, 670, 200);

            fireballDefault.Update(gameTime, 725, 200);
            fireballExplode.Update(gameTime, 770, 200);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ///TODO: TEMP CODE TO DEMONSTRATE SPRITES
            breakableBrick.Draw(spriteBatch);
            floor.Draw(spriteBatch);
            itemBlockUsed.Draw(spriteBatch);
            pipeBase.Draw(spriteBatch);
            pipeTip.Draw(spriteBatch);
            questionBlock.Draw(spriteBatch);
            unbreakableBlock.Draw(spriteBatch);
            flag.Draw(spriteBatch);
            flagPole.Draw(spriteBatch);

            fireballDefault.Draw(spriteBatch);
            fireballExplode.Draw(spriteBatch);

            currentSprite = marioStanding;

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
