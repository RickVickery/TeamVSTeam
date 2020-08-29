using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TeamVsTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            //If(FilePathDetectedThenAsk?LoadFile);
            //Else {
            Display.InitializeGame();
            //}
            bool running = true;
            while (running)
            {
                Display.OrderOfEvents();
            }
        }
        
    }
}
