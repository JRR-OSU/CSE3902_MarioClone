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
        /// <summary>
        /// Spawns all objects needed for a level based on an XML file
        /// </summary>
        /// <param name="filepath">Path to the XML file we should read from for this level</param>
        /// <param name="players">List of players spawned for this level</param>
        /// <param name="enemies">List of enemies spawned for this level</param>
        /// <param name="Tiles">List of tiles spawned for this level</param>
        /// <param name="items">List of items spawned for this level</param>
        /// <returns>True if level loading was successful, false if there was an error.</returns>
        public static bool LoadLevelFromXML(string filepath, out List<IPlayer> players, out List<IEnemy> enemies, out List<ITile> tiles, out List<IItem> items)
        {
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

            bool success = true;

            //Get an xmlreader for our file
            XmlReader reader = XmlReader.Create(filepath);

            //Iterate through all of our file lines, and fill out timelines as applicable
            while (reader.Read())
            {
                //Only care about start elements
                if (!reader.IsStartElement())
                    continue;

                //If this is our root element, get our level type
                if (reader.LocalName == "Root")
                {
                    int posX;

                    if (int.TryParse(reader.GetAttribute("NotAValue"), out posX))
                        Debug.WriteLine("Got value!");
                    else
                        Debug.WriteLine("Failed to get value!");
                    //if (reader.GetAttribute("length") != null)
                    //    showLength = float.Parse(reader.GetAttribute("length"));
                }
                //If this is players block, read in all player elements
                /*else if (reader.LocalName == "Players")
                {
                    //If no child elements, move to next element.
                    if (!reader.ReadToDescendant("Player"))
                        continue;

                    //Add first player element
                    ///TODO: what happens if reader doesn't have element?
                    //if ()
                    {
                        IPlayer pl;
                        int posX, posY;

                        //
                        if (int.TryParse(reader.GetAttribute("posx"), out posX)
                            && int.TryParse(reader.GetAttribute("posy"), out posY)
                            && TryCreatePlayerFromEnum(reader.GetAttribute("type"), out pl))
                        {
                            players.Add(pl);
                        }
                    }

                    //Read all further elements
                    while (reader.ReadToNextSibling("Player"))
                    {
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
                    Debug.WriteLine("Warning: \"" + filepath + "\" level XML file has element of unknown type: " + reader.LocalName);*/
            }

            return success;
        }

        private static bool TryCreatePlayerFromEnum(string pType, out IPlayer pl)
        {
            pl = null;



            return false;
        }
    }
}
