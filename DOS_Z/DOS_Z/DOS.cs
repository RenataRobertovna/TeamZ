using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace DOS_Z
{
    class DOS
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            Dictionary<int, string> catalogs = new Dictionary<int, string>();
            DriveInfo[] drivers = DriveInfo.GetDrives();     // получаем диски
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
=======
            WriteLine("Введите номер диска");
>>>>>>> master
            int numberRoot = Convert.ToInt32(ReadLine());

            string path = "";   // получение пути к диску по его порядковому номеру
            foreach (KeyValuePair<int, string> keyValue in catalogs)     // разбивает дикшиноари "catalog" на пары: Ключ - Значение
            {
                if (keyValue.Key == numberRoot)  // если ключ равен введённому номеру диска, то
                {
                    path = keyValue.Value;     // то путь к диску равен Значению
                }
            }
            WriteLine();
            WriteLine();
            WriteLine();
            Directories(path);
        }

    }
}
