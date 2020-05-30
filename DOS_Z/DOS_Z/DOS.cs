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
            WriteLine("Введите номер диска");
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
        //вывести в отдельный класс
        static void Directories(string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}
