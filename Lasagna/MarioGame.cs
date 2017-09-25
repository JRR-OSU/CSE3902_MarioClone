using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Lasagna
{
    public class MarioGame : Game
    {
        private SpriteBatch spriteBatch;
        private KeyboardController keyControl;
        private List<ITile> tiles = new List<ITile>();
        private List<IEnemy> enemies = new List<IEnemy>();
        private List<IItem> items = new List<IItem>();
        private List<IProjectile> projectiles = new List<IProjectile>();
        private List<IPlayer> players = new List<IPlayer>();

        public MarioGame()
        {
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

            ///TODO: This is temporary for Sprint2
            tiles.Add(new BreakableBrickTile(280, 200));
            tiles.Add(new FlagPoleTile(560, 80));
            tiles.Add(new FloorBlockTile(350, 200));
            tiles.Add(new InvisibleItemBlockTile(140, 200));
            tiles.Add(new QuestionBlockTile(210, 200));
            tiles.Add(new UnbreakableBlockTile(70, 200));
            tiles.Add(new WarpPipeTile(420, 200, 3));

            items.Add(new CoinItem(140, 100));
            items.Add(new FireFlowerItem(70, 100));
            items.Add(new GrowMushroomItem(210, 100));
            items.Add(new LifeMushroomItem(280, 100));
            items.Add(new StarItem(350, 100));

            enemies.Add(new GoombaEnemy(420, 132));
            enemies.Add(new KoopaEnemy(490, 132));

            players.Add(new Mario(200, 300));
        }

        protected override void Update(GameTime gameTime)
        {
            keyControl.Update();

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
