using System;
using System.Collections.Generic;
using System.Text;

namespace RCGC.EverfiReportConverter
{
    class Application : IApplication
    {
        public void Run(string[] args)
        {
            Console.WriteLine("I am running");
            Console.ReadLine();
        }
    }
}
