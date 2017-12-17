using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZipArchivator
{
    class Program
    {
        static readonly int maxThreadCount = Environment.ProcessorCount * 2;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter command (\"-z\" - to compress folder, \"-uz\" - to uncompress folder, \"-q\" - to exit):");
                string command = $@"{Console.ReadLine()}";

                switch (command)
                {
                    case "-z":
                        StartCompressOperation();
                        break;
                    case "-uz":
                        StartDecompressOperation();
                        break;
                    case "-q":
                        return;
                }
            }
        }

        static void StartCompressOperation()
        {
            while (true)
            {
                Console.WriteLine("Enter the folder address to compress all contains files (to exit to the main menu enter: \"-q\"):");
                string folderAddress = $@"{Console.ReadLine()}";

                if (folderAddress == string.Empty)
                {
                    Console.WriteLine("Please, enter directory address!");
                    break;
                }

                if (folderAddress.Equals("-q"))
                {
                   return; 
                }

                var directory = new DirectoryInfo(folderAddress);

                if (!directory.Exists)
                {
                    Console.WriteLine("Directory was not found, please enter the correct address!");
                }

                FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories).Where(p => p.Extension != ".zip" && p.IsReadOnly == false).ToArray();

                Stopwatch watch = new Stopwatch();
                watch.Restart();

                Compress(files);

                Console.WriteLine(watch.ElapsedMilliseconds);

                return;
            }
        }
        static void StartDecompressOperation()
        {
            while (true)
            {
                Console.WriteLine("Enter the folder address to decompress all contains files (to exit enter: \"-q\"):");
                string folderAddress = $@"{Console.ReadLine()}";

                if (folderAddress == string.Empty)
                {
                    Console.WriteLine("Please, enter directory address!");
                    break;
                }

                if (folderAddress.Equals("-q"))
                {
                    return;
                }

                var directory = new DirectoryInfo(folderAddress);

                if (!directory.Exists)
                {
                    Console.WriteLine("Directory was not found, please enter the correct address!");
                }

                FileInfo[] files = directory.GetFiles("*.zip", SearchOption.AllDirectories);

                Stopwatch watch = new Stopwatch();
                watch.Restart();

                Decompress(files);

                Console.WriteLine(watch.ElapsedMilliseconds);

                return;
            }
        }

        static void Compress(FileInfo[] files)
        {
            int concurrency = maxThreadCount;
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < files.Length;)
            {
                int filesLeft = files.Length - i;
                int filesToWork = Math.Min(filesLeft, concurrency);

                for (int j = i; j < i + filesToWork; j++)
                {
                    var newThread = new Thread(CompressFile);
                    newThread.Start(files[j]);
                    threads.Add(newThread);
                }

                for (int j = i; j < i + filesToWork; j++)
                {
                    threads[j].Join();
                }

                i += filesToWork;
            }

        }
        static void Decompress(FileInfo[] files)
        {
            int concurrency = maxThreadCount;
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < files.Length;)
            {
                int filesLeft = files.Length - i;
                int filesToWork = Math.Min(filesLeft, concurrency);

                for (int j = i; j < i + filesToWork; j++)
                {
                    var newThread = new Thread(DecompressFile);
                    newThread.Start(files[j]);
                    threads.Add(newThread);
                }

                for (int j = i; j < i + filesToWork; j++)
                {
                    threads[j].Join();
                }

                i += filesToWork;
            }

        }

        static void CompressFile(object file)
        {
            FileInfo fileInfo = file as FileInfo;
            if (fileInfo == null || File.Exists($"{fileInfo.FullName}.zip"))
                return;

            using (ZipArchive compressStream = ZipFile.Open($"{fileInfo.FullName}.zip", ZipArchiveMode.Create))
            {
                compressStream.CreateEntryFromFile(fileInfo.FullName, fileInfo.FullName + ".zip");

                Console.WriteLine($"File {fileInfo.FullName} compressed into {fileInfo.FullName}.zip");
            }
        }

        static void DecompressFile(object file)
        {
            FileInfo fileInfo = file as FileInfo;
            if (fileInfo == null || !File.Exists($"{fileInfo.FullName}"))
                return;

            using (ZipArchive compressStream = ZipFile.OpenRead($"{fileInfo.FullName}"))
            {
                foreach (ZipArchiveEntry archiveEntry in compressStream.Entries)
                {
                    archiveEntry.ExtractToFile(Path.Combine(fileInfo.DirectoryName, archiveEntry.Name.Replace(".zip", "")));
                }
                Console.WriteLine($"File {fileInfo.FullName} decompressed into {fileInfo.DirectoryName}");
            }
        }

    }
}
