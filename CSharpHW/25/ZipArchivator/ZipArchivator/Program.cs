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

        /* 
         * TODO: 
         * 1. Разделить файлы в папке на maxThreadCount блоков
         * 2. Для каждого файла создать поток и стартовать их
         * 3. Перебрать все потоки и для каждого вызвать JOIN метод, чтобы дождаться завершения рабооты всех фоновых потоков
         * 
         * 
         */
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter the folder address to compress it (to exit enter: \"-q\"):");
                string folderAddress = $@"{Console.ReadLine()}";

                if (folderAddress != string.Empty && folderAddress.Equals("-q"))
                {
                    break;
                }

                var directory = new DirectoryInfo(folderAddress);
                FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories).Where(p => p.Extension != ".zip" && p.IsReadOnly == false).ToArray();

                Stopwatch watch = new Stopwatch();
                watch.Restart();

                Compress(files);

                Console.WriteLine(watch.ElapsedMilliseconds);
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

                for (int j = i; j < files.Length; j++)
                {
                    var newThread = new Thread(CompressFile);
                    newThread.Start(files[j]);
                    threads.Add(newThread);
                }

                for (int j = i; j < files.Length; j++)
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
                ZipArchiveEntry currentEntry = compressStream.CreateEntryFromFile(fileInfo.FullName, fileInfo.FullName + ".zip");

                Console.WriteLine($"File {fileInfo.FullName} compressed into {fileInfo.FullName}.zip");
            }
        }

    }
}
