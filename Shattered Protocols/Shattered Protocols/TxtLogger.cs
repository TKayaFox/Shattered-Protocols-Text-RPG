using Shattered_Protocols.Enumerations;
using Shattered_Protocols.Event_Management;
using Shattered_Protocols.Event_Management.Args;
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

        private string inputLogPath;
        private string logPath;

        public TxtLogger(string fileName)
        {
            // Set file path to the base directory with the given filename
            inputLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + "-Inputs.txt");
            logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + "-Log.txt");

            //Start file logs
            StartFile(inputLogPath);
            StartFile(logPath);

            //Start event management
            ManageMe();
        }

        private static void StartFile(string filePath)
        {

            // Create a new file (will only append if file already exists)
            using (StreamWriter writer = new StreamWriter(filePath,true))
            {
                writer.WriteLine("Log Started: " + DateTime.Now); // Optional initial line
            }

            Console.WriteLine("===================================================");
            Console.WriteLine("Log File created at: " + filePath);
        }

        /// <summary>
        /// Method to add a line of text to the file
        /// </summary>
        /// <param name="line"></param>
        private void AddLine(string line, string filePath)
        {// Append the line to the file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(line);
            }
        }

        //======================== 
        //        Events
        //======================== 

        #region Event Manager
        public void ManageMe()
        {
            //Subscribe to events here, make sure to also include unsubscription
            GameController.Subscribe(EventType.Input, OnNewOutput);
            GameController.Subscribe(EventType.Output, OnNewInput);
        }
        public void UnManageMe()
        {
            //UnSubscribe to events here
            GameController.Unsubscribe(EventType.Output, OnNewOutput);
            GameController.Unsubscribe(EventType.Output, OnNewInput);
        }
        #endregion

        #region Events

        //Triggers Game End Logic
        private void OnNewOutput(EventArgs args)
        {
            //Set default line as an error message that displays if there is issue with input
            String line = "[ERROR: Line Not Found!]";

            //Get string from event args
            line = LineEventString(args);

            //Log output into the gamelog document
            AddLine(line, logPath);
        }

        private void OnNewInput(EventArgs args)
        {
            //Set default line as an error message that displays if there is issue with input
            String line = "[ERROR: Input Data Not Found!]";

            //Get string from event args
            line = LineEventString(args);

            //Display output in console
            Console.WriteLine(line);

            //Log output into the gamelog document
            AddLine(line, logPath);
            AddLine(line, inputLogPath);
        }

        private static string LineEventString(EventArgs args)
        {
            String line = "";

            //Make sure correct eventtype
            if (args is NewLineArgs lineArgs)
            {
                line = lineArgs.Line;
            }

            //Display output in console
            Console.WriteLine(line);
            return line;
        }
        #endregion
    }
}
