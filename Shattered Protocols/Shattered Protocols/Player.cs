using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shattered_Protocols
{
    public class Player
    {
        private Inventory inventory;
        private string name;
        private string description;

        #region Getters and Setters

        /// <summary>
        /// overloaded constructor to fill in player data
        ///     overloaded so if no input provided uses defaults
        /// </summary>
        public Player() : this("Player", "This is you") { }
        public Player(string name, string description)
        {
            this.name = name;
            this.description = description;

            inventory = new Inventory($"{name}'s inventory");
        }

        public Inventory Inventory
        {
            get => inventory;
            set => inventory = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
        #endregion
    }
}
