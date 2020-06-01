using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static System.Console;
namespace DOSz
{
    class DirectoryInformation
    {   //Используем список
        private List<string> catalogs;
        private List<string> files;
        private List<string> catalogsName;
        private List<string> filesName;
        private string CurrentPath; // текущий путь
        private readonly string RootPath; // самый старший путь
        private DriveInfo[] drivers; // список устройств
        public DirectoryInformation()
        {
            catalogs = new List<string>();
            files = new List<string>();
            catalogsName = new List<string>();
            filesName = new List<string>();
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
            for (int i = 0; i < catalogsName.Count; i++)
            {
                WriteLine($"{i + 1}. - {catalogsName[i]}");
            }
            //                    ТО ЖЕ САМОЕ, ЧТО И СНИЗУ
            /*
            foreach (KeyValuePair<int, string> keyValue in catalogsName)
            {
                WriteLine($"{keyValue.Key}. - {keyValue.Value}");
            }
            */
            WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("--------------Файлы--------------");
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < filesName.Count; i++)
            {
                WriteLine($"{i + 1}. - {filesName[i]}");
            }
            WriteLine();
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);

            while (true)
            {
                WriteLine("Жду команду");
                string command = ReadLine();
                try
                {
                    int numberFolder = Convert.ToInt32(command);
                    if (numberFolder == 0)
                    {
                        if (dirInfo.Parent != null)
                        {
                            CurrentPath = dirInfo.Parent.FullName;
                        }
                        else
                        {
                            OriginDirictories();
                        }
                    }
                    else
                    {
                        if (catalogs[numberFolder - 1] != null)
                        {
                            CurrentPath = catalogs[numberFolder - 1];
                        }
                    }
                    Directories();
                    break;
                }
                catch (System.FormatException)
                {
                }
                if (command.Contains("create"))
                {
                    if (command.Contains("file"))
                    {
                        string nameNewFile = command.Substring(12);
                        FileInfo myFile = new FileInfo(CurrentPath + @"\" + nameNewFile);
                        FileStream fs = myFile.Create();
                        fs.Close();
                        WriteLine("Файл создан.");
                    }
                    else if (command.Contains("folder"))
                    {
                        string nameNewFolder = command.Substring(14);
                        DirectoryInfo dirInfo1 = new DirectoryInfo(CurrentPath);
                        if (dirInfo1.Exists)
                        {
                            dirInfo1.CreateSubdirectory(CurrentPath + @"\" + nameNewFolder);
                            WriteLine("Папка создана.");
                        }
                    }
                    else
                    {
                        WriteLine("Неправильная команда.");
                    }
                }
            }
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
                    ClearLists();
                    string[] dirs = Directory.GetDirectories(CurrentPath);
                    string[] files = Directory.GetFiles(CurrentPath);
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dirs[i]);
                        catalogs.Add(dirs[i]);
                        catalogsName.Add(dirInfo.Name);
                    }
                    for (int i = 0; i < files.Length; i++)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(files[i]);
                        this.files.Add(files[i]);
                        filesName.Add(dirInfo.Name);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            { }
        }
        private void OriginDirictories()
        {
            CurrentPath = RootPath;
            ClearLists();
            for (int i = 0; i < drivers.Length; i++)
            {
                if (drivers[i].IsReady)
                {
                    WriteLine($"{i + 1}. - { drivers[i].Name }");
                    catalogs.Add(drivers[i].RootDirectory.ToString());
                    catalogsName.Add(drivers[i].Name);
                }
            }
        }
        private void ClearLists()
        {
            catalogs.Clear();
            catalogsName.Clear();
            files.Clear();
            filesName.Clear();
        }
    }
}
