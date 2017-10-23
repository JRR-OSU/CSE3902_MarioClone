using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

[assembly: CLSCompliant(true)]
namespace Lasagna
{
    public class MarioGame : Game
    {
        private static MarioGame instance;

        public static MarioGame Instance
        {
            get
            {
                if (instance == null)
                    Debug.WriteLine("Error, MarioGame instance not set!! Should be set in Initialize().");

                return instance;
            }
        }

        public Matrix CameraTransform
        {
            get
            {
                return (mainCamera != null) ? mainCamera.Transform : Matrix.Identity;
            }
        }

        private float screenWidth;
        private float screenHeight;

        private SpriteBatch spriteBatch;
        private KeyboardController keyControl;
        private MouseController mouseControl;
        private ISprite levelBackground;
        private ICamera mainCamera;
        private List<ITile> tiles = new List<ITile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IItem> items = new List<IItem>();
        private List<IProjectile> projectiles = new List<IProjectile>();
        private List<IPlayer> players = new List<IPlayer>();

        public MarioGame()
        {
            instance = this;
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

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
            BackgroundSpriteFactory.Instance.LoadAllContent(Content, GraphicsDevice.Viewport.Height / 232 * 3392, GraphicsDevice.Viewport.Height);

            LevelCreator.Instance.LoadLevelFromXML(Environment.CurrentDirectory + "\\Level XML\\Mario_1-1.xml", out levelBackground, out players, out enemies, out tiles, out items);

            IPlayer pl;
            if (players != null && players.Count > 0 && (pl = players.Find(o => o != null)) != null)
                mainCamera = new EdgeControlledCamera(pl.Bounds.X, 0);
            else
                mainCamera = new EdgeControlledCamera(0, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IPlayer player in players)
                if (player != null)
                    player.isCollideGround = false;

            CollisionDetection.Update(players.AsReadOnly(), enemies.AsReadOnly(), tiles.AsReadOnly(), items.AsReadOnly(), projectiles.AsReadOnly());

            keyControl.Update();
            if (players != null && players.Count > 0)
                mouseControl.Update(players[0], enemies.AsReadOnly(), tiles.AsReadOnly());

            if (levelBackground != null)
                levelBackground.Update(gameTime, 0, 0);

            foreach (ITile tile in tiles)
                if (tile != null)
                {
                    if (!(tile is InvisibleItemBlockTile))
                    {
                        tile.Update(gameTime);
                    }
                    //If the tile is an invisible block, then use a different update method.
                    else
                    {
                        foreach (IPlayer player in players)
                            if (player != null)
                            {
                                ((InvisibleItemBlockTile)tile).Update(player, gameTime);
                            }
                    }
                }
            foreach (IProjectile projectile in projectiles)
                if (projectile != null)
                    projectile.Update(gameTime);
            foreach (IEnemy enemy in enemies)
            {
                if (enemy != null)
                {
                    //If this is moving enemy, set isSeen field
                    if (enemy is MovingEnemy)
                        ((MovingEnemy)enemy).isSeen = mainCamera.CanSeeCollider(enemy);

                    enemy.Update(gameTime);
                }
            }
            foreach (IItem item in items)
                if (item != null)
                    item.Update(gameTime);
            foreach (IPlayer player in players)
                if (player != null)
                    player.Update(gameTime);

            bool anyDead = players != null && players.Find(o => o is Mario && ((Mario)o).IsDead) != null;

            if (mainCamera != null && !anyDead)
                mainCamera.Update(players, screenWidth, screenHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (levelBackground != null)
                levelBackground.Draw(spriteBatch);

            foreach (IItem item in items)
                if (item != null)
                    item.Draw(spriteBatch);
            foreach (ITile tile in tiles)
                if (tile != null)
                    tile.Draw(spriteBatch);
            foreach (IEnemy enemy in enemies)
                if (enemy != null)
                    enemy.Draw(spriteBatch);
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

        /// <summary>
        /// Registers given project for update and draw calls
        /// </summary>
        /// <param name="projectile">New projectile to register with system.</param>
        public void RegisterProjectile(IProjectile projectile)
        {
            if (projectile != null && !projectiles.Contains(projectile))
                projectiles.Add(projectile);
        }

        /// <summary>
        /// Removes a given projectile from our update and draw calls system
        /// </summary>
        /// <param name="projectile">Projectile to remove</param>
        public void DeRegisterProjectile(IProjectile projectile)
        {
            if (projectile != null && projectiles.Contains(projectile))
                projectiles.Remove(projectile);
        }
    }
}
