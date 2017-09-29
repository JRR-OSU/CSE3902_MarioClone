using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool success = true;
            players = new List<IPlayer>();
            enemies = new List<IEnemy>();
            tiles = new List<ITile>();
            items = new List<IItem>();



            return success;
        }
    }
}
