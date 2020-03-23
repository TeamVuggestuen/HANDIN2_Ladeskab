using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        private string text = "";
        public void displayCommands(string command)
        {
            text = command;
            Console.WriteLine(command);
        }

        public string Text
        {
            get { return text; }
        }
       
    }
}
