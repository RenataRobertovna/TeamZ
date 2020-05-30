using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;

namespace DOS_Z
{
    class DirectoryInfo
    {
        private Dictionary<int, string> catalogs;
        private Dictionary<int, string> files;

        private DriveInfo[] drivers;

        public DirectoryInfo()
        {
            drivers = DriveInfo.GetDrives();     // получаем диски
            int i = 1;
            foreach (var driver in drivers)                  // проходимся по дискам
            {
                if (driver.IsReady)
                {
                    //Для выбора к какому диску перейти
                    WriteLine($"{i}. - { driver.Name }");
                    catalogs.Add(i, driver.RootDirectory.ToString()); // добавляем в дикшионари порядковый номер диска и его путь
                    i++;
                }
            }
        }

    }
}

