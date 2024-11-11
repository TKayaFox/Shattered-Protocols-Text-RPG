using Shattered_Protocols.Event_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shattered_Protocols
{
    /// <summary>
    /// Txt Logger is a simple object that will take input strings and write them to a txt file.
    /// </summary>
    internal class TxtLogger : IEventManagable
    {
        //EDIT: This DEFINITELY should be event based
        //      Once event based also show:
        //          whether puzzle was able to interpret the command
        //          move all console output handling to TxtLogger, possibly find a better name.
        //              make a specific event for WriteLine that replaces Console.Writeline
        //              Writeline instead, and it writes to console AND to text doc

        private string filePath;

        public TxtLogger(string fileName)
        {
            // Set file path to the base directory with the given filename
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + ".txt");

            // Create a new file, or overwrite if it already exists
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("File created: " + DateTime.Now); // Optional initial line
            }

            Console.WriteLine("Log File created at: " + filePath);
        }

        /// <summary>
        /// Method to add a line of text to the file
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(string line)
        {// Append the line to the file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(line);
            }
        }

        //======================== 
        //        Events
        //======================== 

        #region Event Management
        public void ManageMe()
        {
            //Subscribe to events here, make sure to also include unsubscription
        }
        public void UnManageMe()
        {
            //UnSubscribe to events here
        }
        #endregion
        #region Events

        #endregion
    }
}
