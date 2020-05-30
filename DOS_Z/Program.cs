using System;
using static System.Console;

namespace DOSz
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInformation dirInfo = new DirectoryInformation();
            while (true)
            {
                dirInfo.ShowCurrentCatalogs();
            }
        }
    }
}
