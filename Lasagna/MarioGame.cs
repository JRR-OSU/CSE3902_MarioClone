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
        private MouseController mouseControl;
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
            mouseControl = new MouseController();

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
            CollisionDetection.Instance.Update(players.AsReadOnly(), enemies.AsReadOnly(), tiles.AsReadOnly(), items.AsReadOnly());

            keyControl.Update();
            if (players != null && players.Count > 0)
                mouseControl.Update(players[0], enemies, tiles);

            if (levelBackground != null)
                levelBackground.Update(gameTime, 0, 0);

            foreach (ITile tile in tiles)
                if (tile != null)
                    tile.Update(gameTime);
            foreach (IProjectile projectile in projectiles)
                if (projectile != null)
                    projectile.Update(gameTime);
            foreach (IEnemy enemy in enemies)
                if (enemy != null)
                    enemy.Update(gameTime);
            foreach (IItem item in items)
                if (item != null)
                    item.Update(gameTime);
            foreach (IPlayer player in players)
                if (player != null)
                    player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (levelBackground != null)
                levelBackground.Draw(spriteBatch);

            foreach (ITile tile in tiles)
                if (tile != null)
                    tile.Draw(spriteBatch);
            foreach (IEnemy enemy in enemies)
                if (enemy != null)
                    enemy.Draw(spriteBatch);
            foreach (IItem item in items)
                if (item != null)
                    item.Draw(spriteBatch);
            foreach (IProjectile projectile in projectiles)
                if (projectile != null)
                    projectile.Draw(spriteBatch);
            foreach (IPlayer player in players)
                if (player != null)
                    player.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
