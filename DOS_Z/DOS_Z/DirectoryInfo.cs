using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;

namespace DOS_Z
{
    class DirectoryInformation
    {
        private Dictionary<int, string> catalogs;
        private Dictionary<int, string> files;
<<<<<<< HEAD
        private Dictionary<int, string> catalogsName;
        private Dictionary<int, string> filesName;
        private string CurrentPath;

        private DriveInfo[] drivers;

        public DirectoryInformation()
        {
            catalogs = new Dictionary<int, string>();
            files = new Dictionary<int, string>();
            catalogsName = new Dictionary<int, string>();
            filesName = new Dictionary<int, string>();
            drivers = DriveInfo.GetDrives();                // получаем диски
            OriginDirictories();
        }
        public void ShowCurrentCatalogs() //показ каталога
        {
            Console.Clear();
            WriteLine($"Текущая директория: ---> { CurrentPath }");
            WriteLine("___Каталог___");
            foreach (KeyValuePair<int, string> keyValue in catalogsName)
            {
                WriteLine($"{keyValue.Key}. - {keyValue.Value}");
            }
            WriteLine("___Файлы___");
            foreach (KeyValuePair<int, string> keyValue in filesName)
            {
                WriteLine($"{keyValue.Key}. - {keyValue.Value}");
            }

            WriteLine("Введите номер директории для перехода далее");
            int numberRoot = Convert.ToInt32(ReadLine());

            foreach (KeyValuePair<int, string> keyValue in catalogs)     // разбивает дикшиноари "catalog" на пары: Ключ - Значение
            {
                if (keyValue.Key == numberRoot)  // если ключ равен введённому номеру диска, то
                {
                    CurrentPath = keyValue.Value;     // то путь к диску равен Значению
                }
            }
            Directories();
        }
        private void ShowFilesInCatalog() //показ файлов
        {

        }
        public void ChangeCurrentCatalog(int num) //переход в следующий каталог
        {

        }
        private void Directories()
        {
            try
            {
                catalogs.Clear();
                catalogsName.Clear();
                files.Clear();
                filesName.Clear();
                int i = 1;
                string[] dirs = Directory.GetDirectories(CurrentPath);
                foreach (string dir in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    if (Directory.Exists(dir))
                    {
                        catalogs.Add(i, dir);
                        catalogsName.Add(i, dirInfo.Name);
                        i++;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
        private void OriginDirictories()
        {
            CurrentPath = "Список дисков!";
            int i = 1;
            foreach (var driver in drivers) // проходимся по дискам
=======

        private DriveInfo[] drivers;

        public DirectoryInfo()
        {
            drivers = DriveInfo.GetDrives();     // получаем диски
            int i = 1;
            foreach (var driver in drivers)                  // проходимся по дискам
>>>>>>> master
            {
                if (driver.IsReady)
                {
                    //Для выбора к какому диску перейти
                    WriteLine($"{i}. - { driver.Name }");
                    catalogs.Add(i, driver.RootDirectory.ToString()); // добавляем в дикшионари порядковый номер диска и его путь
<<<<<<< HEAD
                    catalogsName.Add(i, driver.Name);
=======
>>>>>>> master
                    i++;
                }
            }
        }
<<<<<<< HEAD
    }
}
/*
string dirName = "C:\\";
 
if (Directory.Exists(dirName))
{
    Console.WriteLine("Подкаталоги:");
    string[] dirs = Directory.GetDirectories(dirName);
    foreach (string s in dirs)
    {
        Console.WriteLine(s);
    }
    Console.WriteLine();
    Console.WriteLine("Файлы:");
    string[] files = Directory.GetFiles(dirName);
    foreach (string s in files)
    {
        Console.WriteLine(s);
    }
}
    tring dirName = "C:\\Program Files";

DirectoryInfo dirInfo = new DirectoryInfo(dirName);

Console.WriteLine($"Название каталога: {dirInfo.Name}");
Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
*/
=======

    }
}

>>>>>>> master
