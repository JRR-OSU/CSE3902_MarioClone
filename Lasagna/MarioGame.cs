using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class MarioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardController keyControl;
        private ISprite levelBackground;
        private List<ITile> tiles = new List<ITile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IItem> items = new List<IItem>();
        private List<IProjectile> projectiles = new List<IProjectile>();
        private List<IPlayer> players = new List<IPlayer>();

        public MarioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
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
            BackgroundSpriteFactory.Instance.LoadAllContent(Content, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            LevelCreator.Instance.LoadLevelFromXML(Environment.CurrentDirectory + "\\Level XML\\Mario_1-1.xml", out levelBackground, out players, out enemies, out tiles, out items);
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();

            if (levelBackground != null)
                levelBackground.Update(gameTime, 0, 0);

            foreach (ITile t in tiles)
                t.Update(gameTime);
            foreach (IProjectile t in projectiles)
                t.Update(gameTime);
            foreach (IEnemy t in enemies)
                t.Update(gameTime);
            foreach (IItem t in items)
                t.Update(gameTime);
            foreach (IPlayer p in players)
                p.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (levelBackground != null)
                levelBackground.Draw(spriteBatch);

            foreach (ITile t in tiles)
                t.Draw(spriteBatch);
            foreach (IEnemy t in enemies)
                t.Draw(spriteBatch);
            foreach (IItem t in items)
                t.Draw(spriteBatch);
            foreach (IProjectile t in projectiles)
                t.Draw(spriteBatch);
            foreach (IPlayer p in players)
                p.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
