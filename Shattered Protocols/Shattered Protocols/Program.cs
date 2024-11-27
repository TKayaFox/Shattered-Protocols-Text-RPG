using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    internal class Program
    {
        ///Starts the Game Logic
        public static void Main()
        {
            //Start the Game Logic
            Game game = new Game();

            // Wait for user input before closing
            Console.WriteLine("Press enter to close game");
            Console.Read();
        }
    }
}
