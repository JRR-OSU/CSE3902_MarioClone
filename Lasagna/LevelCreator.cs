using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Lasagna
{
    public class LevelCreator
    {
        private const string XmlFileExtension = ".xml";
        private const string BadXmlError1 = "Error! Invalid filepath given for loading level: \"";
        private const string BadXmlError2 = "\"";
        private const string BadXmlElementError1 = "Warning: \"";
        private const string BadXmlElementError2 = "\" level XML file has element of unknown type: ";
        private const string BadPlayerTypeError = "Invalid type passed for creating player: ";
        private const string BadEnemyTypeError = "Invalid type passed for creating enemy: ";
        private const string BadTileTypeError = "Invalid type passed for creating tile: ";
        private const string BadItemTypeError = "Invalid type passed for creating item: ";
        private const string RootLocalName = "Root";
        private const string LevelTypeAttr = "leveltype";
        private const string CameraTypeAttr = "cameratype";
        private const string CameraXPosAttr = "cameraxpos";
        private const string CameraYPosAttr = "cameraypos";
        private const string PlayersLocalName = "Players";
        private const string PlayerChildElementName = "Player";
        private const string EnemiesLocalName = "Enemies";
        private const string EnemyChildElementName = "Enemy";
        private const string TilesLocalName = "Tiles";
        private const string TileChildElementName = "Tile";
        private const string PosXAttr = "posx";
        private const string PosYAttr = "posy";
        private const string TypeAttr = "type";
        private const string RepeatAttr = "repeat";
        private const string RepeatSpaceAttr = "repeatspace";
        private const string CameraXAttr = "cameraX";
        private const string CameraYAttr = "cameraY";
        private const string FacingAttr = "facing";
        private const string HeightAttr = "height";
        private const string WarpDestAttr = "warpDest";
        private const string WarpSourceAttr = "warpSource";
        private const string CoinsAttr = "coins";
        private const string ItemAttr = "item";
        private const string ItemsLocalName = "Items";
        private const string ItemChildElementName = "Item";
        private const int Five = 5;
        private const int One = 1;
        private const int Zero = 0;

        #region Object Type Dictionaries

        private readonly Dictionary<LevelType, ISprite> levelBackdrops = new Dictionary<LevelType, ISprite>(CreateLevelTypesDictionary());
        private readonly Dictionary<CameraType, Func<int, int, ICamera>> cameraTypes = new Dictionary<CameraType, Func<int, int, ICamera>>(CreateCameraTypesDictionary());
        private readonly Dictionary<PlayerType, Func<uint, int, int, IPlayer>> playerTypes = new Dictionary<PlayerType, Func<uint, int, int, IPlayer>>(CreatePlayerTypesDictionary());
        private readonly Dictionary<EnemyType, Func<int, int, IEnemy>> enemyTypes = new Dictionary<EnemyType, Func<int, int, IEnemy>>(CreateEnemyTypesDictionary());
        private readonly Dictionary<TileType, Func<Direction, int, int, int, Vector2, string, string, IItem[], ITile>> tileTypes = new Dictionary<TileType, Func<Direction, int, int, int, Vector2, string, string, IItem[], ITile>>(CreateTileTypesDictionary());
        private readonly Dictionary<ItemType, Func<int, int, IItem>> itemTypes = new Dictionary<ItemType, Func<int, int, IItem>>(CreateItemTypesDictionary());

        private static Dictionary<LevelType, ISprite> CreateLevelTypesDictionary()
        {
            Dictionary<LevelType, ISprite> newDictionary = new Dictionary<LevelType, ISprite>()
            {
                { LevelType.MarioClear, BackgroundSpriteFactory.Instance.CreateBackground_MarioClear() }
            };

            return newDictionary;
        }

        private static Dictionary<CameraType, Func<int, int, ICamera>> CreateCameraTypesDictionary()
        {
            Dictionary<CameraType, Func<int, int, ICamera>> newDictionary = new Dictionary<CameraType, Func<int, int, ICamera>>()
            {
                { CameraType.EdgeControlled, (int posX, int posY) => new EdgeControlledCamera(posX, posY) },
                { CameraType.Fixed, (int posX, int posY) => new FixedCamera(posX, posY) }
            };

            return newDictionary;
        }

        private static Dictionary<PlayerType, Func<uint, int, int, IPlayer>> CreatePlayerTypesDictionary()
        {
            Dictionary<PlayerType, Func<uint, int, int, IPlayer>> newDictionary = new Dictionary<PlayerType, Func<uint, int, int, IPlayer>>()
            {
                { PlayerType.Mario, (uint playerNumber, int posX, int posY) => new Mario(playerNumber, posX, posY) },
                { PlayerType.Luigi, (uint playerNumber, int posX, int posY) => new Mario(playerNumber, posX, posY) }
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

        private static Dictionary<TileType, Func<Direction, int, int, int, Vector2, string, string, IItem[], ITile>> CreateTileTypesDictionary()
        {
            Dictionary<TileType, Func<Direction, int, int, int, Vector2, string, string, IItem[], ITile>> newDictionary = new Dictionary<TileType, Func<Direction, int, int, int, Vector2, string, string, IItem[], ITile>>()
            {
                { TileType.Brick, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] blockItems) => new BreakableBrickTile(posX, posY, blockItems)},
                { TileType.Flag, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] notUsed5) => new FlagPoleTile(posX, posY)},
                { TileType.Floor, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] notUsed5) => new FloorBlockTile(posX, posY)},
                { TileType.InvisibleBlock, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] blockItems) => new InvisibleItemBlockTile(posX, posY, blockItems)},
                { TileType.QuestionBlock, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] blockItems) => new QuestionBlockTile(posX, posY, blockItems)},
                { TileType.UnbreakableBlock, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] notUsed5) => new UnbreakableBlockTile(posX, posY)},
                { TileType.Pipe, (Direction facing, int posX, int posY, int height, Vector2 warpForcesCamPos, string warpSource, string warpDest, IItem[] notUsed3) => new WarpPipeTile(posX, posY, facing, height, warpSource, warpDest, warpForcesCamPos)},
                { TileType.UndergroundBrick, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] notUsed5) => new UndergroundBrickTile(posX, posY)},
                { TileType.UndergroundFloor, (Direction notUsed4, int posX, int posY, int notUsed1, Vector2 notUsed6, string notUsed2, string notUsed3, IItem[] notUsed5) => new UndergroundFloorBlockTile(posX, posY)},
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

        private delegate bool ObjectFromEnumDelegate<T>(string type, int posX, int posY, int childIndex, out List<T> objects);

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
        /// <param name="levelBackground">Sprite for background of level</param>
        /// <param name="mainCamera">Camera created for this level.</param>
        /// <param name="players">List of players spawned for this level</param>
        /// <param name="enemies">List of enemies spawned for this level</param>
        /// <param name="Tiles">List of tiles spawned for this level</param>
        /// <param name="items">List of items spawned for this level</param>
        /// <returns>True if level loading was successful, false if there was an error.</returns>
        public bool LoadLevelFromXML(string filepath, out ISprite levelBackground, out ICamera mainCamera, out List<IPlayer> players, out List<IEnemy> enemies, out List<ITile> tiles, out List<IItem> items)
        {
            levelBackground = null;
            mainCamera = null;
            players = new List<IPlayer>();
            enemies = new List<IEnemy>();
            tiles = new List<ITile>();
            items = new List<IItem>();

            //If we were given a bad file, exit.
            if (!filepath.EndsWith(XmlFileExtension) || !File.Exists(filepath))
            {
                Debug.WriteLine(BadXmlError1 + filepath + BadXmlError2);
                return false;
            }

            XmlReader reader = XmlReader.Create(filepath);

            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                if (reader.LocalName == RootLocalName)
                {
                    LevelType t;
                    //Get level background
                    if (TryGetLevelTypeFromEnum(reader.GetAttribute(LevelTypeAttr), out t) && levelBackdrops.ContainsKey(t))
                        levelBackground = levelBackdrops[t];
                    //Create camera based on camera type
                    CameraType cType;
                    if (TryGetCameraTypeFromEnum(reader.GetAttribute(CameraTypeAttr), out cType) && cameraTypes.ContainsKey(cType))
                    {
                        int xPos, yPos;
                        if (int.TryParse(reader.GetAttribute(CameraXPosAttr), out xPos) && int.TryParse(reader.GetAttribute(CameraYPosAttr), out yPos))
                            mainCamera = cameraTypes[cType].Invoke(xPos, yPos);
                        else
                            mainCamera = cameraTypes[cType].Invoke(Zero, Zero);
                    }
                }
                //NOTE: The order of players in the XML file is the player number they are (first occurence is player 1, second is player 2, etc.)
                else if (reader.LocalName == PlayersLocalName)
                    RetrieveAllChildElementsFromReader<IPlayer>(ref reader, PlayerChildElementName, TryCreatePlayerFromEnum, out players);
                else if (reader.LocalName == EnemiesLocalName)
                    RetrieveAllChildElementsFromReader<IEnemy>(ref reader, EnemyChildElementName, TryCreateEnemyFromEnum, out enemies);
                //Tile uses its own logic temporarily, until we add in optional parameters to RetrieveAllChildElementsFromReader() next sprint.
                else if (reader.LocalName == TilesLocalName)
                {
                    if (!reader.ReadToDescendant(TileChildElementName))
                        continue;

                    //Add first tile element
                    List<ITile> newTiles = new List<ITile>();
                    List<IItem> newItems = new List<IItem>();
                    int posX, posY;
                    if (int.TryParse(reader.GetAttribute(PosXAttr), out posX)
                        && int.TryParse(reader.GetAttribute(PosYAttr), out posY)
                        && TryCreateTileFromEnum(reader, reader.GetAttribute(TypeAttr), reader.GetAttribute(RepeatAttr), reader.GetAttribute(RepeatSpaceAttr), posX, posY, out newTiles, out newItems))
                    {
                        tiles.AddRange(newTiles);
                        items.AddRange(newItems);
                    }

                    //Add all subsequent elements
                    while (reader.ReadToNextSibling(TileChildElementName))
                    {
                        if (int.TryParse(reader.GetAttribute(PosXAttr), out posX)
                           && int.TryParse(reader.GetAttribute(PosYAttr), out posY)
                           && TryCreateTileFromEnum(reader, reader.GetAttribute(TypeAttr), reader.GetAttribute(RepeatAttr), reader.GetAttribute(RepeatSpaceAttr), posX, posY, out newTiles, out newItems))
                        {
                            tiles.AddRange(newTiles);
                            items.AddRange(newItems);
                        }
                    }
                }
                else if (reader.LocalName == ItemsLocalName)
                {
                    List<IItem> newItems;
                    RetrieveAllChildElementsFromReader<IItem>(ref reader, ItemChildElementName, TryCreateItemFromEnum, out newItems);
                    items.AddRange(newItems);
                }
                else
                    Debug.WriteLine(BadXmlElementError1 + filepath + BadXmlElementError2 + reader.LocalName);
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
            if (int.TryParse(reader.GetAttribute(PosXAttr), out posX)
                && int.TryParse(reader.GetAttribute(PosYAttr), out posY)
                && objCreationMethod(reader.GetAttribute(TypeAttr), posX, posY, childElements.Count, out objects))
            {
                childElements.AddRange(objects);
            }

            //Add all subsequent sibling elements
            while (reader.ReadToNextSibling(elementName))
            {
                if (int.TryParse(reader.GetAttribute(PosXAttr), out posX)
                   && int.TryParse(reader.GetAttribute(PosYAttr), out posY)
                   && objCreationMethod(reader.GetAttribute(TypeAttr), posX, posY, childElements.Count, out objects))
                {
                    childElements.AddRange(objects);
                }
            }
        }

        private static bool TryGetLevelTypeFromEnum(string lType, out LevelType t)
        {
            t = Zero;
            return !string.IsNullOrEmpty(lType) && Enum.TryParse(lType, out t);
        }

        private static bool TryGetCameraTypeFromEnum(string cType, out CameraType t)
        {
            t = Zero;
            return !string.IsNullOrEmpty(cType) && Enum.TryParse(cType, out t);
        }

        private bool TryCreatePlayerFromEnum(string pType, int posX, int posY, int childIndex, out List<IPlayer> players)
        {
            players = new List<IPlayer>();
            PlayerType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(pType) || !Enum.TryParse(pType, out t) || !playerTypes.ContainsKey(t))
            {
                Debug.WriteLine(BadPlayerTypeError + pType);
                return false;
            }

            players.Add(playerTypes[t].Invoke((uint)childIndex, posX, posY));
            return true;
        }

        private bool TryCreateEnemyFromEnum(string eType, int posX, int posY, int childIndex, out List<IEnemy> enemies)
        {
            enemies = new List<IEnemy>();
            EnemyType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(eType) || !Enum.TryParse(eType, out t) || !enemyTypes.ContainsKey(t))
            {
                Debug.WriteLine(BadEnemyTypeError + eType);
                return false;
            }

            enemies.Add(enemyTypes[t].Invoke(posX, posY));
            return true;
        }

        private bool TryCreateTileFromEnum(XmlReader reader, string tType, string repeatTimes, string repeatSpace, int posX, int posY, out List<ITile> tiles, out List<IItem> items)
        {
            tiles = new List<ITile>();
            items = new List<IItem>();
            TileType t;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(tType) || !Enum.TryParse(tType, out t) || !tileTypes.ContainsKey(t))
            {
                Debug.WriteLine(BadTileTypeError + tType);
                return false;
            }

            List<IItem> blockItems = new List<IItem>();
            Direction pipeFacing = Direction.Up;
            int pipeHeight = -One;
            string warpSource = String.Empty;
            string warpDest = String.Empty;
            Vector2 warpForcesCamPos = Vector2.Zero;
            int blockItemCount = -One;
            string itemStr = String.Empty;

            //If this is pipe, get optional params from reader
            if (t == TileType.Pipe)
            {
                int camX, camY;
                if (int.TryParse(reader.GetAttribute(CameraXAttr), out camX) && int.TryParse(reader.GetAttribute(CameraYAttr), out camY))
                    warpForcesCamPos = new Vector2(camX, camY);

                Enum.TryParse(reader.GetAttribute(FacingAttr), out pipeFacing);
                int.TryParse(reader.GetAttribute(HeightAttr), out pipeHeight);
                pipeHeight = Math.Max(Zero, pipeHeight);
                warpDest = reader.GetAttribute(WarpDestAttr);
                warpSource = reader.GetAttribute(WarpSourceAttr);

                tiles.Add(tileTypes[t].Invoke(pipeFacing, posX, posY, pipeHeight, warpForcesCamPos, warpSource, warpDest, blockItems.ToArray()));
            }
            //If this is question, invisible, or brick block, optional params from reader
            else if (t == TileType.Brick || t == TileType.InvisibleBlock || t == TileType.QuestionBlock)
            {
                IItem blockItem;
                int.TryParse(reader.GetAttribute(CoinsAttr), out blockItemCount);
                blockItemCount = Math.Max(Zero, blockItemCount);
                itemStr = reader.GetAttribute(ItemAttr);
                if (TryCreateItemFromEnum(itemStr, posX, posY, out blockItem))
                {
                    blockItems.Add(blockItem);
                    //If item is mushroom, create fireflower as well
                    if (blockItem is GrowMushroomItem)
                        blockItems.Add(itemTypes[ItemType.FireFlower].Invoke(posX, posY));

                    //Duplicate for number of items needed
                    for (int i = One; i < blockItemCount; i++)
                    {
                        if (TryCreateItemFromEnum(itemStr, posX, posY, out blockItem))
                        {
                            blockItems.Add(blockItem);
                            //If item is mushroom, create fireflower as well
                            if (blockItem is GrowMushroomItem)
                                blockItems.Add(itemTypes[ItemType.FireFlower].Invoke(posX, posY));
                        }
                    }

                    items.AddRange(blockItems);
                }

                tiles.Add(tileTypes[t].Invoke(pipeFacing, posX, posY, pipeHeight, warpForcesCamPos, warpSource, warpDest, blockItems.ToArray()));
            }
            //Else create generic tile
            else
                tiles.Add(tileTypes[t].Invoke(Direction.Up, posX, posY, -One, Vector2.Zero, String.Empty, String.Empty, blockItems.ToArray()));

            //If this element has a repeat field, and it has a valid integer, repeat this tile according to the field.
            //Each repeated tile is spawned to the right of the base tile.
            ///TODO: Maybe change this to take a directional param later?
            int rTimes,
                rSpace = Zero;
            if (tiles.Count > Zero && tiles[Zero] != null && !string.IsNullOrEmpty(repeatTimes) && int.TryParse(repeatTimes, out rTimes))
            {
                if (rTimes < Zero)
                    rTimes = Zero;
                //Try to get spacing between tiles, defaulting to no space.
                if (!string.IsNullOrEmpty(repeatSpace))
                    rSpace = int.Parse(repeatSpace);
                if (rSpace < Zero)
                    rSpace = Zero;

                //Add tile width to spacing
                rSpace += tiles[Zero].Bounds.Width;

                //Spawn each subsequent tile
                for (int i = One; i <= rTimes; i++)
                {
                    List<IItem> newItems = new List<IItem>();
                    IItem newItem = null;
                    for (int bItem = Zero; bItem < blockItemCount; bItem++)
                    {
                        if (TryCreateItemFromEnum(itemStr, posX + (rSpace * i), posY, out newItem))
                        {
                            newItems.Add(newItem);
                            //If item is mushroom, create fireflower as well
                            if (newItem is GrowMushroomItem)
                                newItems.Add(itemTypes[ItemType.FireFlower].Invoke(posX, posY));
                        }
                    }

                    items.AddRange(newItems);

                    tiles.Add(tileTypes[t].Invoke(pipeFacing, posX + (rSpace * i), posY, pipeHeight, warpForcesCamPos, warpSource, warpDest, newItems.ToArray()));
                }
            }

            return true;
        }                

        private bool TryCreateItemFromEnum(string iType, int posX, int posY, out IItem item)
        {
            ItemType t;
            item = null;

            //If passed null parameter, or can't cast to type, or we don't have a delegate for type, exit.
            if (string.IsNullOrEmpty(iType) || !Enum.TryParse(iType, out t) || !itemTypes.ContainsKey(t))
            {
                if (!string.IsNullOrEmpty(iType))
                    Debug.WriteLine(BadItemTypeError + iType);
                return false;
            }

            item = itemTypes[t].Invoke(posX, posY);

            return true;
        }

        private bool TryCreateItemFromEnum(string iType, int posX, int posY, int childIndex, out List<IItem> items)
        {
            items = new List<IItem>();
            IItem item;

            if (!TryCreateItemFromEnum(iType, posX, posY, out item))
                return false;

            items.Add(item);
            return true;
        }
    }
}