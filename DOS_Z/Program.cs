using System;
using System.IO;
using static System.Console;

namespace DOSz
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives) 
            {
                WriteLine("Имя диска:" + drive.Name);
                WriteLine("Файловая система: " + drive.DriveFormat);
                WriteLine("Тип диска: " + drive.DriveType);
                if (drive.IsReady)
                {
                    WriteLine("Объем доступного свободного места (в байтах): " + drive.AvailableFreeSpace);
                    WriteLine("Корневой каталог диска: " + drive.RootDirectory);
                    WriteLine("Объем диска: " + drive.TotalFreeSpace);
                    WriteLine("Метка тома: " + drive.VolumeLabel);
                }
                WriteLine();
            }
<<<<<<< HEAD
            FileInfo fileInf = new FileInfo("c:\folder");
            // fileInf = null
            FileInfo fileInf2 = new FileInfo("c:\folder.txt");
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
=======
            */
>>>>>>> master
            DirectoryInformation dirInfo = new DirectoryInformation();
            while (true)
            {
                dirInfo.ShowCurrentCatalogs();
            }
        }
    }
}
