using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lasagna
{
    public class LevelCreator
    {
        private static Dictionary<LevelType, ISprite> levelBackdrops = new Dictionary<LevelType, ISprite>()
        {
            { LevelType.MarioClear, BackgroundSpriteFactory.Instance.CreateBackground_MarioClear() }
        };


        /// <summary>
        /// Spawns all objects needed for a level based on an XML file
        /// </summary>
        /// <param name="filepath">Path to the XML file we should read from for this level</param>
        /// <param name="players">List of players spawned for this level</param>
        /// <param name="enemies">List of enemies spawned for this level</param>
        /// <param name="Tiles">List of tiles spawned for this level</param>
        /// <param name="items">List of items spawned for this level</param>
        /// <returns>True if level loading was successful, false if there was an error.</returns>
        public static bool LoadLevelFromXML(string filepath, out ISprite levelBackground, out List<IPlayer> players, out List<IEnemy> enemies, out List<ITile> tiles, out List<IItem> items)
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
                {
                    if (!reader.ReadToDescendant("Player"))
                        continue;

                    //Add first player element
                    IPlayer pl;
                    int posX, posY;
                    if (int.TryParse(reader.GetAttribute("posx"), out posX)
                        && int.TryParse(reader.GetAttribute("posy"), out posY)
                        && TryCreatePlayerFromEnum(reader.GetAttribute("type"), posX, posY, out pl))
                    {
                        players.Add(pl);
                    }

                    //Add all subsequent elements
                    while (reader.ReadToNextSibling("Player"))
                    {
                        if (int.TryParse(reader.GetAttribute("posx"), out posX)
                            && int.TryParse(reader.GetAttribute("posy"), out posY)
                            && TryCreatePlayerFromEnum(reader.GetAttribute("type"), posX, posY, out pl))
                        {
                            players.Add(pl);
                        }
                    }
                }
                else if (reader.LocalName == "Enemies")
                {

                }
                else if (reader.LocalName == "Tiles")
                {

                }
                else if (reader.LocalName == "Items")
                {

                }
                else
                    Debug.WriteLine("Warning: \"" + filepath + "\" level XML file has element of unknown type: " + reader.LocalName); */
            }

            return true;
        }

        private static bool TryGetLevelTypeFromEnum(string lType, out LevelType t)
        {
            t = 0;
            return !string.IsNullOrEmpty(lType) && Enum.TryParse(lType, out t);
        }

        private static bool TryCreatePlayerFromEnum(string pType, int posX, int posY, out IPlayer pl)
        {
            pl = null;

            if ()

            return false;
        }
    }
}
