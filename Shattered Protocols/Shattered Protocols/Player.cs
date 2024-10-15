using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    public class Player
    {
        private Inventory inventory = new Inventory();
        private string name = "Player";
        private string description = "This is you";

        #region Getters and Setters
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
