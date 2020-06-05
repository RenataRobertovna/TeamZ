using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
namespace DOSz
{
    class DirectoryInformation
    {
        //Используем список
        public List<string> catalogs { private set; get; }
        public List<string> files { private set; get; }
        public List<string> catalogsName { private set; get; }
        public List<string> filesName { private set; get; }
        public List<string> message_cash { private set; get; }
        public List<string> greenNames { private set; get; }
        public bool isCopied { private set; get; }
        public string CopiedPath { private set; get; }
        public string CopiedName { private set; get; }
        public string CurrentPath { private set; get; } // текущий путь

        private readonly string RootPath;   // самый старший путь
        public DriveInfo[] drivers { private set; get; } // список устройств
        public DirectoryInformation()
        {
            isCopied = false;
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
        public void ShowCurrentCatalogs()               //показ каталога
        {
            string previousPath = CurrentPath;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Write("{0, -45}", "Текущая директория: ----------------------> ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine($"{ CurrentPath }");
            Console.ForegroundColor = ConsoleColor.Blue;
            Write("{0, -45}", "Текущий скопированный/вырезанный путь: ---> ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine($"{ CopiedPath }");
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
                if (i < catalogsName.Count)
                {
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
                    Write("{0, -5}", "");
                    Write("{0, 20}\t\t\t", "");
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
            WriteLine();
            for (int i = 0; i < message_cash.Count; i++)
            {
                WriteLine(message_cash[i]);
            }
            message_cash.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);
            while (true)
            {
                message_cash.Add("Жду команду (help - для просмотра доступных команд)");
                WriteLine("Жду команду (help - для просмотра доступных команд)");
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
                        if (numberFolder <= catalogs.Count)
                        {
                            CurrentPath = catalogs[numberFolder - 1];
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
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
                              "move   file/folder <номер> \n" +
                              "copy   file        <номер> \n" +
                              "insert - вставить file/folder \n" +
                              "Для перехода на след каталог\n" +
                              "нужно ввести номер каталога.\n" +
                              "(0 - перейти в родительский каталог)\n" +
                              "=================================");
                }
                else if (command.Contains("move"))
                {
                    if (command.Contains("folder"))
                    {
                        int numberFolder = Convert.ToInt32(command.Substring(12));
                        if (numberFolder <= catalogs.Count)
                        {
                            CopiedPath = catalogs[numberFolder - 1];
                            CopiedName = catalogsName[numberFolder - 1];
                            greenNames.Add(CopiedName);
                            Directories(previousPath);
                            isCopied = false;
                            return;
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
                        }
                    }
                    else if (command.Contains("file"))
                    {
                        int numberFile = Convert.ToInt32(command.Substring(10));
                        if (numberFile <= files.Count)
                        {
                            CopiedPath = files[numberFile - 1];
                            CopiedName = filesName[numberFile - 1];
                            greenNames.Add(CopiedName);
                            Directories(previousPath);
                            isCopied = false;
                            return;
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
                        }
                    }
                }
                else if (command.Contains("insert"))
                {
                    if (!isCopied)
                    {
                        if (CopiedPath == null) continue;
                        FileInfo fileInf = new FileInfo(CopiedPath);
                        if (fileInf.Exists)
                        {
                            fileInf.MoveTo(CurrentPath + @"\" + CopiedName);
                            WriteLine("Файл перемещен успешно.");
                            message_cash.Add("Файл перемещен успешно.");
                            greenNames.Add(CopiedName);
                            Directories(previousPath);
                            CopiedPath = "";
                            CopiedName = "";
                            return;
                        }
                        else
                        {
                            DirectoryInfo dirInfo1 = new DirectoryInfo(CopiedPath);
                            if (dirInfo1.Exists && Directory.Exists(CurrentPath + @"\" + CopiedName) == false)
                            {
                                dirInfo1.MoveTo(CurrentPath + @"\" + CopiedName);
                                WriteLine("Папка перемещена успешно.");
                                message_cash.Add("Папка перемещена успешно.");
                                greenNames.Add(CopiedName);
                                CopiedPath = "";
                                CopiedName = "";
                                Directories(previousPath);
                                return;
                            }
                        }
                    }
                    else
                    {
                        FileInfo fileInf1 = new FileInfo(CopiedPath);
                        if (fileInf1.Exists)
                        {
                            fileInf1.CopyTo(CurrentPath + @"\" + CopiedName, true);
                            WriteLine("Файл успешно вставлен.");
                            message_cash.Add("Файл успешно вставлен.");
                            greenNames.Add(CopiedName);
                            Directories(previousPath);
                            CopiedPath = "";
                            CopiedName = "";
                            isCopied = false;
                            return;
                        }
                    }
                }
                else if (command.Contains("delete"))
                {
                    if (command.Contains("file"))
                    {
                        int numberFile = Convert.ToInt32(command.Substring(12));
                        if (numberFile <= files.Count)
                        {
                            FileInfo fileInf = new FileInfo(files[numberFile - 1]);
                            if (fileInf.Exists)
                            {
                                fileInf.Delete();
                                WriteLine("Файл удален успешно!");
                                message_cash.Add("Файл удален успешно!");
                                Directories(previousPath);
                                return;
                            }
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
                        }
                    }
                    else if (command.Contains("folder"))
                    {
                        int numberFolder = Convert.ToInt32(command.Substring(14));
                        if (numberFolder <= catalogs.Count)
                        {
                            try
                            {
                                DirectoryInfo dirInfo1 = new DirectoryInfo(catalogs[numberFolder - 1]);
                                dirInfo1.Delete(true);
                                WriteLine("Каталог удален");
                                message_cash.Add("Каталог удален");
                                Directories(previousPath);
                                return;

                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
                        }
                    }
                }
                else if (command.Contains("copy"))
                {
                    if (command.Contains("folder"))
                    {
                        WriteLine("Нельзя копировать папку.");
                    }
                    else if (command.Contains("file"))
                    {
                        int numberFile = Convert.ToInt32(command.Substring(10));
                        if (numberFile <= files.Count)
                        {
                            CopiedPath = files[numberFile - 1];
                            CopiedName = filesName[numberFile - 1];
                            greenNames.Add(CopiedName);
                            isCopied = true;
                            WriteLine("Файл успешно скопирован.");
                            message_cash.Add("Файл успешно скопирован.");
                            Directories(previousPath);
                            return;
                        }
                        else
                        {
                            WriteLine("Несуществующий порядковый номер файла/каталога");
                        }
                    }
                }
            }
        }

        private void Directories(string previousPath)
        {
            try
            {
                if (!CurrentPath.Equals(RootPath))
                {
                    if (!CurrentPath.Equals(previousPath))
                    {
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
        public bool DeleteFile(int numberFile, string previousPath)
        {
            FileInfo fileInf = new FileInfo(files[numberFile]);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                Directories(previousPath);
                return true;
            }
            return false;
        }
        public bool CreateFolder(string nameNewFolder, string previousPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);
            if (dirInfo.Exists)
            {
                dirInfo.CreateSubdirectory(@nameNewFolder);
                WriteLine("Папка создана.");
                message_cash.Add("Папка создана.");
                greenNames.Add(new DirectoryInfo(CurrentPath + @"\" + nameNewFolder).Name);
                Directories(previousPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateFile(string nameNewFile, string previousPath)
        {
            FileInfo myFile = new FileInfo(CurrentPath + @"\" + nameNewFile);
            FileStream fs = myFile.Create();
            fs.Close();
            greenNames.Add(myFile.Name);
            Directories(previousPath);
        }
        public void CopyFile(int numberFile)
        {
            if (numberFile <= filesName.Count)
            {
                CopiedPath = files[numberFile - 1];
                CopiedName = filesName[numberFile - 1];
                greenNames.Add(CopiedName);
                isCopied = true;
            }
        }
        public bool ChangeCatalog(int numberFolder, string previousPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);
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
                if (numberFolder <= catalogs.Count)
                {
                    CurrentPath = catalogs[numberFolder - 1];
                }
                else
                {
                    return false;
                }
            }
            message_cash.Clear();
            Directories(previousPath);
            return true;
        }
        public bool InsertFileHere(string previousPath)
        {
            FileInfo fileInf = new FileInfo(CopiedPath);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(CurrentPath + @"\" + CopiedName, true);
                greenNames.Add(CopiedName);
                Directories(previousPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool MoveFolderHere(string previousPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CopiedPath);
            if (dirInfo.Exists && Directory.Exists(CurrentPath + @"\" + CopiedName) == false)
            {
                dirInfo.MoveTo(CurrentPath + @"\" + CopiedName);
                greenNames.Add(CopiedName);
                CopiedPath = "";
                CopiedName = "";
                Directories(previousPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void MoveFileHere(string previousPath)
        {
            FileInfo fileInf = new FileInfo(CopiedPath);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(CurrentPath + @"\" + CopiedName);
                greenNames.Add(CopiedName);
                Directories(previousPath);
            }
        }
        public string GetHelpInfo()
        {
        return "==========Подсказка==============\n" +
                "create file/folder <наз-е файла> \n" +
                "delete file/folder <номер> \n" +
                "move   file/folder <номер> \n" +
                "copy   file        <номер> \n" +
                "insert - вставить file/folder \n" +
                "Для перехода на след каталог\n" +
                "нужно ввести номер каталога.\n" +
                "(0 - перейти в родительский каталог)\n" +
                "=================================";
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