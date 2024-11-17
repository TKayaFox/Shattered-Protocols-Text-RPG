using System;
using System.Collections.Generic;

namespace Shattered_Protocols
{
    public class GameState
    {
        public string CurrentRoom { get; set; }
        public List<string> SolvedPuzzles { get; set; }
        public List<string> Inventory { get; set; }

        public GameState()
        {
            CurrentRoom = "FrontDesk"; // Default starting room
            SolvedPuzzles = new List<string>();
            Inventory = new List<string>();
        }
    }
}
