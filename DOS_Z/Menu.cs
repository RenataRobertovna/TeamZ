using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DOSz
{
    public static class Menu
    {
        public static bool ChooseObject()
        {
            WriteLine("Выберите с чем вы хотите дальше работать: папку(/folder) или файл(/file)");
            string temp = ReadLine();
            if (temp == "folder")
            {
                return true;
            }
            return false;
        }
    }
    
}
