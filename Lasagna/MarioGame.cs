using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lasagna
{
    public class MarioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;
        private KeyboardController keyControl;
        private List<ITile> tiles = new List<ITile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IItem> items = new List<IItem>();
        private List<IProjectile> projectiles = new List<IProjectile>();

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

            ///TODO: This is temporary for Sprint2
            tiles.Add(new BreakableBrickTile(280, 200));
            //tiles.Add(new FlagPoleTile(670, 200));
            tiles.Add(new FloorBlockTile(350, 200));
            //tiles.Add(new InvisibleItemBlockTile(280, 200));
            tiles.Add(new QuestionBlockTile(210, 200));
            tiles.Add(new UnbreakableBlockTile(70, 200));
            //tiles.Add(new WarpPipeTile(420, 200));
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();

            foreach (ITile t in tiles)
                t.Update(gameTime);
            foreach (IProjectile t in projectiles)
                t.Update(gameTime);
            /*foreach (IEnemy t in enemies)
                t.Update(gameTime);
            foreach (IItem t in items)
                t.Update(gameTime);*/

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            foreach (ITile t in tiles)
                t.Draw(spriteBatch);
            foreach (IEnemy t in enemies)
                t.Draw(spriteBatch);
            foreach (IItem t in items)
                t.Draw(spriteBatch);
            foreach (IProjectile t in projectiles)
                t.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit()
        {
            Exit();
        }
    }
}
