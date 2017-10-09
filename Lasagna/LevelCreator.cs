﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Lasagna
{
    public class LevelCreator
    {
        #region Object Type Dictionaries

        private readonly Dictionary<LevelType, ISprite> levelBackdrops = new Dictionary<LevelType, ISprite>(CreateLevelTypesDictionary());
        private readonly Dictionary<PlayerType, Func<int, int, IPlayer>> playerTypes = new Dictionary<PlayerType, Func<int, int, IPlayer>>(CreatePlayerTypesDictionary());
        private readonly Dictionary<EnemyType, Func<int, int, IEnemy>> enemyTypes = new Dictionary<EnemyType, Func<int, int, IEnemy>>(CreateEnemyTypesDictionary());
        private readonly Dictionary<TileType, Func<int, int, ITile>> tileTypes = new Dictionary<TileType, Func<int, int, ITile>>(CreateTileTypesDictionary());
        private readonly Dictionary<ItemType, Func<int, int, IItem>> itemTypes = new Dictionary<ItemType, Func<int, int, IItem>>(CreateItemTypesDictionary());

        private static Dictionary<LevelType, ISprite> CreateLevelTypesDictionary()
        {
            Dictionary<LevelType, ISprite> newDictionary = new Dictionary<LevelType, ISprite>()
            {
                { LevelType.MarioClear, BackgroundSpriteFactory.Instance.CreateBackground_MarioClear() }
            };

            return newDictionary;
        }

        private static Dictionary<PlayerType, Func<int, int, IPlayer>> CreatePlayerTypesDictionary()
        {
            Dictionary< PlayerType, Func<int, int, IPlayer>> newDictionary = new Dictionary<PlayerType, Func<int, int, IPlayer>>()
            {
                { PlayerType.Mario, (int posX, int posY) => new Mario(posX, posY) }
            };

            return newDictionary;
        }
        private static Dictionary<EnemyType, Func<int, int, IEnemy>> CreateEnemyTypesDictionary()
        {
            Dictionary<EnemyType, Func<int, int, IEnemy>> newDictionary = new Dictionary<EnemyType, Func<int, int, IEnemy>>()
            {
                { EnemyType.Goomba, (int posX, int posY) => new GoombaEnemy(posX, posY) },
                { EnemyType.Koopa, (int posX, int posY) => new KoopaEnemy(posX, posY) }
            };

            return newDictionary;
        }

        private static Dictionary<TileType, Func<int, int, ITile>> CreateTileTypesDictionary()
        {
            Dictionary<TileType, Func<int, int, ITile>> newDictionary = new Dictionary<TileType, Func<int, int, ITile>>()
            {
                { TileType.Brick, (int posX, int posY) => new BreakableBrickTile(posX, posY)},
                { TileType.Flag, (int posX, int posY) => new FlagPoleTile(posX, posY)},
                { TileType.Floor, (int posX, int posY) => new FloorBlockTile(posX, posY)},
                { TileType.InvisibleBlock, (int posX, int posY) => new InvisibleItemBlockTile(posX, posY)},
                { TileType.QuestionBlock, (int posX, int posY) => new QuestionBlockTile(posX, posY)},
                { TileType.UnbreakableBlock, (int posX, int posY) => new UnbreakableBlockTile(posX, posY)},
                { TileType.Pipe, (int posX, int posY) => new WarpPipeTile(posX, posY, 3)}
            };

            return newDictionary;
        }
        private static Dictionary<ItemType, Func<int, int, IItem>> CreateItemTypesDictionary()
        {
            Dictionary<ItemType, Func<int, int, IItem>> newDictionary = new Dictionary<ItemType, Func<int, int, IItem>>()
            {
                { ItemType.Coin, (int posX, int posY) => new CoinItem(posX, posY)},
                { ItemType.FireFlower, (int posX, int posY) => new FireFlowerItem(posX, posY)},
                { ItemType.GrowMushroom, (int posX, int posY) => new GrowMushroomItem(posX, posY)},
                { ItemType.LifeMushroom, (int posX, int posY) => new LifeMushroomItem(posX, posY)},
                { ItemType.Star, (int posX, int posY) => new StarItem(posX, posY)},
            };

            return newDictionary;
        }

        #endregion

        private delegate bool ObjectFromEnumDelegate<T>(string type, int posX, int posY, out List<T> objects);

        private static LevelCreator instance;

        public static LevelCreator Instance
        {
            get
            {
                if (instance == null)
                    instance = new LevelCreator();

                return instance;
            }
        }

        /// <summary>
        /// Spawns all objects needed for a level based on an XML file
        /// </summary>
        /// <param name="filepath">Path to the XML file we should read from for this level</param>
        /// <param name="players">List of players spawned for this level</param>
        /// <param name="enemies">List of enemies spawned for this level</param>
        /// <param name="Tiles">List of tiles spawned for this level</param>
        /// <param name="items">List of items spawned for this level</param>
        /// <returns>True if level loading was successful, false if there was an error.</returns>
        public bool LoadLevelFromXML(string filepath, out ISprite levelBackground, out List<IPlayer> players, out List<IEnemy> enemies, out List<ITile> tiles, out List<IItem> items)
        {
            levelBackground = null;
            players = new List<IPlayer>();
            enemies = new List<IEnemy>();
            tiles = new List<ITile>();
            items = new List<IItem>();

            //If we were given a bad file, exit.
            if (!filepath.EndsWith(".xml") || !File.Exists(filepath))
            {
                Debug.WriteLine("Error! Invalid filepath given for loading level: \"" + filepath + "\"");
                return false;
            }

            XmlReader reader = XmlReader.Create(filepath);

            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                if (reader.LocalName == "Root")
                {
                    LevelType t;
                    if (TryGetLevelTypeFromEnum(reader.GetAttribute("leveltype"), out t) && levelBackdrops.ContainsKey(t))
                        levelBackground = levelBackdrops[t];
                }
                else if (reader.LocalName == "Players")
                    RetrieveAllChildElementsFromReader<IPlayer>(ref reader, "Player", TryCreatePlayerFromEnum, out players);
                else if (reader.LocalName == "Enemies")
                    RetrieveAllChildElementsFromReader<IEnemy>(ref reader, "Enemy", TryCreateEnemyFromEnum, out enemies);
                //Tile uses its own logic temporarily, until we add in optional parameters to RetrieveAllChildElementsFromReader() next sprint.
                else if (reader.LocalName == "Tiles")
                {
                    if (!reader.ReadToDescendant("Tile"))
                        continue;

                    //Add first tile element
                    List<ITile> newTiles = new List<ITile>();
                    int posX, posY;
                    if (int.TryParse(reader.GetAttribute("posx"), out posX)
                        && int.TryParse(reader.GetAttribute("posy"), out posY)
                        && TryCreateTileFromEnum(reader.GetAttribute("type"), reader.GetAttribute("repeat"), reader.GetAttribute("repeatspace"), posX, posY, out newTiles))
                    {
                        tiles.AddRange(newTiles);
                    }

                    //Add all subsequent elements
                    while (reader.ReadToNextSibling("Tile"))
                    {
                        if (int.TryParse(reader.GetAttribute("posx"), out posX)
                           && int.TryParse(reader.GetAttribute("posy"), out posY)
                           && TryCreateTileFromEnum(reader.GetAttribute("type"), reader.GetAttribute("repeat"), reader.GetAttribute("repeatspace"), posX, posY, out newTiles))
                        {
                            tiles.AddRange(newTiles);
                        }
                    }
                }
                else if (reader.LocalName == "Items")
                    RetrieveAllChildElementsFromReader<IItem>(ref reader, "Item", TryCreateItemFromEnum, out items);
                else
                    Debug.WriteLine("Warning: \"" + filepath + "\" level XML file has element of unknown type: " + reader.LocalName);
            }

            return true;
        }

        //TODO: Extend this to allow for optional paramters, such as what Tile takes.
        private static void RetrieveAllChildElementsFromReader<T>(ref XmlReader reader, string elementName, ObjectFromEnumDelegate<T> objCreationMethod, out List<T> childElements)
        {
            childElements = new List<T>();

            if (!reader.ReadToDescendant(elementName))
                return;

            //Add first element
            List<T> objects;
            int posX, posY;
            if (int.TryParse(reader.GetAttribute("posx"), out posX)
                && int.TryParse(reader.GetAttribute("posy"), out posY)
                && objCreationMethod(reader.GetAttribute("type"), posX, posY, out objects))
            {
                childElements.AddRange(objects);
            }

            //Add all subsequent sibling elements
            while (reader.ReadToNextSibling(elementName))
            {
                if (int.TryParse(reader.GetAttribute("posx"), out posX)
                   && int.TryParse(reader.GetAttribute("posy"), out posY)
                   && objCreationMethod(reader.GetAttribute("type"), posX, posY, out objects))
                {
                    childElements.AddRange(objects);
                }
            }
        }

        private static bool TryGetLevelTypeFromEnum(string lType, out LevelType t)
        {
            t = 0;
            return !string.IsNullOrEmpty(lType) && Enum.TryParse(lType, out t);
        }

        private bool TryCreatePlayerFromEnum(string pType, int posX, int posY, out List<IPlayer> players)
        {
            players = new List<IPlayer>();
            PlayerType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(pType) || !Enum.TryParse(pType, out t) || !playerTypes.ContainsKey(t))
            {
                Debug.WriteLine("Invalid type passed for creating player: " + pType);
                return false;
            }

            players.Add(playerTypes[t].Invoke(posX, posY));
            return true;
        }

        private bool TryCreateEnemyFromEnum(string eType, int posX, int posY, out List<IEnemy> enemies)
        {
            enemies = new List<IEnemy>();
            EnemyType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(eType) || !Enum.TryParse(eType, out t) || !enemyTypes.ContainsKey(t))
            {
                Debug.WriteLine("Invalid type passed for creating enemy: " + eType);
                return false;
            }

            enemies.Add(enemyTypes[t].Invoke(posX, posY));
            return true;
        }
        private bool TryCreateTileFromEnum(string tType, string repeatTimes, string repeatSpace, int posX, int posY, out List<ITile> tiles)
        {
            tiles = new List<ITile>();
            TileType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(tType) || !Enum.TryParse(tType, out t) || !tileTypes.ContainsKey(t))
            {
                Debug.WriteLine("Invalid type passed for creating tile: " + tType);
                return false;
            }

            //Create base tile
            tiles.Add(tileTypes[t].Invoke(posX, posY));

            //If this element has a repeat field, and it has a valid integer, repeat this tile according to the field.
            //Each repeated tile is spawned to the right of the base tile.
            ///TODO: Maybe change this to take a directional param later?
            int rTimes,
                rSpace = 0;
            if (tiles.Count > 0 && tiles[0] != null && !string.IsNullOrEmpty(repeatTimes) && int.TryParse(repeatTimes, out rTimes))
            {
                if (rTimes < 0)
                    rTimes = 0;
                //Try to get spacing between tiles, defaulting to no space.
                if (!string.IsNullOrEmpty(repeatSpace))
                    rSpace = int.Parse(repeatSpace);
                if (rSpace < 0)
                    rSpace = 0;

                //Add tile width to spacing
                rSpace += tiles[0].Bounds.Width;

                //Spawn each subsequent tile
                for (int i = 1; i <= rTimes; i++)
                    tiles.Add(tileTypes[t].Invoke(posX + (rSpace * i), posY));
            }

            return true;
        }

        private bool TryCreateItemFromEnum(string iType, int posX, int posY, out List<IItem> items)
        {
            items = new List<IItem>();
            ItemType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(iType) || !Enum.TryParse(iType, out t) || !itemTypes.ContainsKey(t))
            {
                Debug.WriteLine("Invalid type passed for creating item: " + iType);
                return false;
            }

            items.Add(itemTypes[t].Invoke(posX, posY));
            return true;
        }
    }
}
