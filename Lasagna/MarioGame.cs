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
        private const string ContentDirectory = "Content";
        private const string InstanceError = "Error, MarioGame instance not set!! Should be set in Initialize().";
        private const string Level1XMLPath = "\\Level XML\\Mario_1-1.xml";
        private const int Two = 2;
        private const int Zero = 0;

        private static MarioGame instance;

        public static MarioGame Instance
        {
            get
            {
                if (instance == null)
                    Debug.WriteLine(InstanceError);

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
        //private MouseController mouseControl;
        private ISprite levelBackground;
        private ICamera mainCamera;
        private List<ITile> tiles = new List<ITile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IItem> items = new List<IItem>();
        private List<IProjectile> projectiles = new List<IProjectile>();
        private List<IPlayer> players = new List<IPlayer>();
        private HUD hud;
        private SpriteFont font;
        private bool paused;
        //Fields for warping
        private bool warping;
        private int warpDestX;
        private int warpDestY;
        private int warpDestCamPosX;
        private int warpDestCamPosY;
        private Direction warpDestPipeFacing;
        private bool deathScreen = true;
        private const int deathScreenCount = 120;
        private int deathScreenTimer = Zero;

        public MarioGame()
        {
            instance = this;
            new GraphicsDeviceManager(this);
            Content.RootDirectory = ContentDirectory;
        }

        protected override void Initialize()
        {
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            keyControl = new KeyboardController();
            // mouseControl = new MouseController();

            //Subscribe to events
            MarioEvents.OnQuit += OnQuit;
            MarioEvents.OnPause += OnPauseGame;

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
            BackgroundSpriteFactory.Instance.LoadAllContent(Content, GraphicsDevice.Viewport.Height);
            hud = new HUD();

            LevelCreator.Instance.LoadLevelFromXML(Environment.CurrentDirectory + Level1XMLPath, out levelBackground, out players, out enemies, out tiles, out items);
            font = Content.Load<SpriteFont>("Fonts/HUD");
            IPlayer pl;
            if (players != null && players.Count > Zero && (pl = players.Find(o => o != null)) != null)
                mainCamera = new EdgeControlledCamera(pl.Bounds.X, Zero);
            else
                mainCamera = new EdgeControlledCamera(Zero, Zero);

                  
        }

        protected override void Update(GameTime gameTime)
        {
            //If game is paused, exit.
            if (paused)
                return;

            hud.Update();
            foreach (IPlayer player in players)
                if (player != null)
                    player.isCollideGround = false;

            keyControl.Update();
            //if (players != null && players.Count > Zero)
            //   mouseControl.Update(players[Zero], enemies.AsReadOnly(), tiles.AsReadOnly());

            CollisionDetection.Update(players.AsReadOnly(), enemies.AsReadOnly(), tiles.AsReadOnly(), items.AsReadOnly(), projectiles.AsReadOnly());

            if (levelBackground != null)
                levelBackground.Update(gameTime, Zero, Zero);

            foreach (ITile tile in tiles)
                if (tile != null)
                {
                    if (!(tile is InvisibleItemBlockTile) || !(tile is QuestionBlockTile) || !(tile is BreakableBrickTile))
                    {
                        tile.Update(gameTime);
                    }
                    //If the tile is an invisible block, then use a different update method.
                    else
                    {
                        foreach (IPlayer player in players)
                            if (player != null)
                            {
                                if (tile is InvisibleItemBlockTile)
                                {
                                    ((InvisibleItemBlockTile)tile).Update(player, gameTime);
                                }
                                else if (tile is QuestionBlockTile)
                                {
                                    ((QuestionBlockTile)tile).Update(player, gameTime);
                                }
                                else if (tile is BreakableBrickTile)
                                {
                                    ((BreakableBrickTile)tile).Update(player, gameTime);
                                }
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

            if (mainCamera != null && !anyDead && !warping)
                mainCamera.Update(players, screenWidth, screenHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (deathScreen)
            {
                DisplayDeathScreen();
                return;
            }

            if (levelBackground != null)
                levelBackground.Draw(spriteBatch);

            //If we're warping, draw under everything else
            if (warping)
            {
                foreach (IPlayer player in players)
                    if (player != null)
                        player.Draw(spriteBatch);
            }
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
            //If we're not warping, draw over everything else
        
            if (!warping)
            {
                foreach (IPlayer player in players)
                    if (player != null)
                        player.Draw(spriteBatch);
            }
            hud.Draw(spriteBatch,font,deathScreen);
            base.Draw(gameTime);
        }

        //Event handlers
        private void OnQuit(object sender, EventArgs e)
        {
            Exit();
        }

        private void OnPauseGame(object sender, EventArgs e)
        {
            paused = !paused;
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

        /// <summary>
        /// Attempts too warp to the given target. If target is valid, plays warp animation and moves.
        /// </summary>
        /// <param name="warpDest">ID of pipe we want too warp to</param>
        /// <param name="warpCamPosX">X position too force the camera to for this warp</param>
        /// <param name="warpCamPosY">Y position too force the camera to for this warp</param>
        /// <param name="sourcePipeFacing">Direction source pipe is facing</param>
        public void TryWarp(string warpDest, int warpCamPosX, int warpCamPosY, Direction sourcePipeFacing)
        {
            if (string.IsNullOrEmpty(warpDest) || warping)
                return;

            foreach (ITile tile in tiles)
            {
                if (tile == null || !(tile is WarpPipeTile) || !((WarpPipeTile)tile).WarpSource.Equals(warpDest))
                    continue;

                Direction dir = ((WarpPipeTile)tile).PipeDirection;
                if (dir == Direction.Up || dir == Direction.Down)
                {
                    warpDestX = tile.Bounds.X + tile.Bounds.Width / Two;
                    warpDestY = (dir == Direction.Up) ? tile.Bounds.Y : tile.Bounds.Bottom;
                }
                else
                {
                    warpDestX = (dir == Direction.Left) ? tile.Bounds.X : tile.Bounds.Right;
                    warpDestY = tile.Bounds.Y + tile.Bounds.Height / Two;
                }

                //Begin warp process
                warpDestPipeFacing = dir;
                warpDestCamPosX = warpCamPosX;
                warpDestCamPosY = warpCamPosY;
                warping = true;

                Direction animDir = Direction.Down;

                if (sourcePipeFacing == Direction.Down)
                    animDir = Direction.Up;
                else if (sourcePipeFacing == Direction.Left)
                    animDir = Direction.Right;
                else if (sourcePipeFacing == Direction.Right)
                    animDir = Direction.Left;

                //Start animating each player
                foreach (IPlayer pl in players)
                    if (pl != null)
                        pl.BeginWarpAnimation(animDir, true);

                break;
            }
        }

        public void SecondWarpPhase()
        {
            if (!warping)
                return;

            //move each player to warp dest, and begin playing exit pipe animation.
            foreach (IPlayer pl in players)
            {
                if (pl != null)
                {
                    int offsetX = Zero, offsetY = Zero;

                    if (warpDestPipeFacing == Direction.Down)
                        offsetY = -pl.Bounds.Height;
                    else if (warpDestPipeFacing == Direction.Right)
                        offsetX = -pl.Bounds.Width;

                    pl.SetPosition(warpDestX + offsetX, warpDestY + offsetY);
                    pl.BeginWarpAnimation(warpDestPipeFacing, false);
                }
            }

            //Force main camera to the necessary view
            if (mainCamera != null)
                mainCamera.ForcePosition(warpDestCamPosX, warpDestCamPosY);
        }

        public void FinishWarp()
        {
            warping = false;
        }

        public void TriggerDeathSequence()
        {
            deathScreen = true;
        }

        private void DisplayDeathScreen()
        {
            hud.Draw(spriteBatch, font, deathScreen);
            if (deathScreenTimer < deathScreenCount)
                deathScreenTimer++;
            else
            {
                deathScreenTimer = Zero;
                deathScreen = false;
            }
        }
    }
}
