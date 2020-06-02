using System;
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
        private List<string> message_cash;
        private List<string> greenNames;
        private string CurrentPath; // текущий путь
        private readonly string RootPath; // самый старший путь
        private DriveInfo[] drivers; // список устройств
        public DirectoryInformation()
        {
            greenNames = new List<string>();
            message_cash = new List<string>();
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
            string previousPath = CurrentPath;
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
            WriteLine("\t\t{0}\t\t\t\t\t{1}", "Каталоги", "Файлы");
            WriteLine();
            for (int i = 0; i < catalogsName.Count || i < filesName.Count; i++)
            {
                string folderName = "";
                string fileName = "";
                bool fold = false;
                bool file = false;
                if (i < catalogsName.Count) {
                    fold = true;
                    folderName = catalogsName[i];
                    if (folderName.Length >= 20)
                    {
                        folderName = folderName.Substring(0, 20);
                    }
                }
                if (i < filesName.Count)
                {
                    file = true;
                    fileName = filesName[i];
                    if (fileName.Length >= 20)
                    {
                        fileName = fileName.Substring(0, 20);
                    }
                }
                if (fold && file)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Write("{0, -5}", (i + 1).ToString() + ".");
                    if (greenNames.Contains(folderName))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Write("{0, -20}\t\t\t", folderName);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Write("{0, -5}", (i + 1).ToString() + ".");
                    if (greenNames.Contains(fileName))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    WriteLine("{0, -20}", fileName);
                }
                else if (fold && !file)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Write("{0, -5}", (i + 1).ToString() + ".");
                    if (greenNames.Contains(folderName))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    WriteLine("{0, -20}\t\t\t", folderName);
                }
                else if (!fold && file)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Write("{0, -5}", (i + 1).ToString() + ".");
                    if (greenNames.Contains(fileName))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    WriteLine("{0, -20}", fileName);
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            /*
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < catalogsName.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Write($"{i + 1}.");
                if (greenNames.Contains(catalogsName[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Write($" - {catalogsName[i]} ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    WriteLine($" <---- Созданная папка");
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($" - {catalogsName[i]} ");
                }
            }
            WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("--------------Файлы--------------");
            WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < filesName.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Write($"{i + 1}.");
                if (greenNames.Contains(filesName[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Write($" - {filesName[i]} ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    WriteLine($" <---- Созданный файл");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($" - {filesName[i]}");
                }
            }
            */
            WriteLine();

            for(int i = 0; i < message_cash.Count; i++)
            {
                WriteLine(message_cash[i]);
            }
            message_cash.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);

            while (true)
            {
                message_cash.Add("Жду команду");
                WriteLine("Жду команду");
                string command = ReadLine();
                message_cash.Add(command);
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
                    message_cash.Clear();
                    Directories(previousPath);
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
                        message_cash.Add("Файл создан.");
                        greenNames.Add(myFile.Name);
                        Directories(previousPath);
                        return;
                    }
                    else if (command.Contains("folder"))
                    {
                        string nameNewFolder = command.Substring(14);
                        DirectoryInfo dirInfo1 = new DirectoryInfo(CurrentPath);
                        if (dirInfo1.Exists)
                        {
                            dirInfo1.CreateSubdirectory(@nameNewFolder);
                            WriteLine("Папка создана.");
                            message_cash.Add("Папка создана.");
                            greenNames.Add(new DirectoryInfo(CurrentPath + @"\" + nameNewFolder).Name);
                            Directories(previousPath);
                            return;
                        }
                        
                    }
                    else
                    {
                        WriteLine("Неправильная команда.");
                        message_cash.Add("Неправильная команда.");
                    }
                }
                else if (command.Contains("help"))
                {
                    WriteLine("==========Подсказка==============\n" +
                              "create file/folder <наз-е файла> \n" +
                              "delete file/folder <номер> \n" +
                              "copy file/folder <номер>  \n" +
                              "Для перехода на след каталог\n" +
                              "нужно ввести номер каталога. \n" +
                              "(0 - перейти в родительский каталог)\n" +
                              "=================================");
                }
            }
        }
        //показ файлов
        private void ShowFilesInCatalog() { }
        //переход в следующий каталог
        public void ChangeCurrentCatalog() { }
        private void Directories(string previousPath)
        {
            try
            {
                if (!CurrentPath.Equals(RootPath))
                {
                    if (!CurrentPath.Equals(previousPath)) {
                        greenNames.Clear();
                    }
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
