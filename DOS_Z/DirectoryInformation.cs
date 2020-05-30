using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.Console;
namespace DOSz
{
    class DirectoryInformation
    {
        private Dictionary<int, string> catalogs;
        private Dictionary<int, string> files;
        private Dictionary<int, string> catalogsName;
        private Dictionary<int, string> filesName;
        private List<string> rootDirectories;
        private string CurrentPath;
        private readonly string RootPath;
        private DriveInfo[] drivers;
        public DirectoryInformation()
        {
            rootDirectories = new List<string>();
            catalogs = new Dictionary<int, string>();
            files = new Dictionary<int, string>();
            catalogsName = new Dictionary<int, string>();
            filesName = new Dictionary<int, string>();
            drivers = DriveInfo.GetDrives(); // получаем диски
            RootPath = "Список доступных дисков.";
            OriginDirictories();
        }
        public void ShowCurrentCatalogs() //показ каталога
        {
            Console.Clear();
            Write($"Текущая директория: ---> ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteLine($"{ CurrentPath }");
            Console.ResetColor();
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine($"{0}. - НАЗАД");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("--------------Каталоги--------------");
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (KeyValuePair<int, string> keyValue in catalogsName)
            {
                WriteLine($"{keyValue.Key}. - {keyValue.Value}");
            }
            WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("--------------Файлы--------------");
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (KeyValuePair<int, string> keyValue in filesName)
            {
                WriteLine($"{keyValue.Key}. - {keyValue.Value}");
            }
            WriteLine();
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);

            WriteLine("Введите номер директории для перехода далее");
            int numberRoot = Convert.ToInt32(ReadLine());
            if (numberRoot == 0)
            {

                if (dirInfo.Parent != null)
                {
                    CurrentPath = dirInfo.Parent.FullName;
                    
                } else
                {
                    OriginDirictories();
                }
            }
            else
            {
                foreach (KeyValuePair<int, string> keyValue in catalogs)     // разбивает дикшиноари "catalog" на пары: Ключ - Значение
                {
                    if (keyValue.Key == numberRoot)  // если ключ равен введённому номеру диска, то
                    {
                        CurrentPath = keyValue.Value;     // то путь к диску равен Значению
                    }
                }
            }
            Directories();
           
        }
        //показ файлов
        private void ShowFilesInCatalog() { }
        //переход в следующий каталог
        public void ChangeCurrentCatalog() { }
        private void Directories()
        {
            try
            {
                if (!CurrentPath.Equals(RootPath))
                {
                    catalogs.Clear();
                    catalogsName.Clear();
                    this.files.Clear();
                    filesName.Clear();
                    int i = 1;
                    string[] dirs = Directory.GetDirectories(CurrentPath);
                    string[] files = Directory.GetFiles(CurrentPath);
                    foreach (string dir in dirs)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        {
                            catalogs.Add(i, dir);
                            catalogsName.Add(i, dirInfo.Name);
                            i++;
                        }
                    }
                    i = 1;
                    foreach (string file in files)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(file);
                        {
                            this.files.Add(i, file);
                            filesName.Add(i, dirInfo.Name);
                            i++;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
        private void OriginDirictories()
        {
            CurrentPath = RootPath;
            int i = 1;
            catalogs.Clear();
            catalogsName.Clear();
            files.Clear();
            filesName.Clear();
            rootDirectories.Clear();
            foreach (var driver in drivers) // проходимся по дискам
            {
                if (driver.IsReady)
                {
                    //Для выбора к какому диску перейти
                    WriteLine($"{i}. - { driver.Name }");
                    catalogs.Add(i, driver.RootDirectory.ToString()); // добавляем в дикшионари порядковый номер диска и его путь
                    catalogsName.Add(i, driver.Name);
                    rootDirectories.Add(driver.RootDirectory.ToString());
                    i++;
                }
            }
        }
    }
}