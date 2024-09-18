using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_Text_RPG
{
    internal class Program
    {
        ArrayList roomList = new ArrayList();

        ///Starts the Game Logic
        public void Main ()
        {
            Console.WriteLine("Hello, World!");

            //  use the current time as a seed to ensure Randomness between program runs
            int seed = DateTime.Now.Millisecond;
            Random randomizer = new Random(seed);

            //Start the Game Logic
            //  Note: Eventually add an intro, start menu etc
            Game game = new Game();
        }
    }
}
