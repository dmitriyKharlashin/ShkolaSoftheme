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
                
                Stopwatch watch = new Stopwatch();
                watch.Restart();
                Compress(folderAddress);
                Console.WriteLine(watch.ElapsedMilliseconds);

                //Decompress(folderAddress);
            }
        }

        static void Compress(string folderAddress)
        {
            if (folderAddress != string.Empty && Directory.Exists(folderAddress))
            {
                string[] subdirectories = Directory.GetDirectories(folderAddress);

                if (subdirectories.Length > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        Compress(subdirectory);
                    }
                }

                string[] files = Directory.GetFiles(folderAddress).Where(p => new FileInfo(p).Extension != ".zip").ToArray();

                if (files.Length > 0)
                {
                    // simple compression
                    //CompressFile(files);

                    // compress with simple threading
                    Thread thread = new Thread(new ParameterizedThreadStart(CompressFile));
                    thread.Start(files);

                    // compress with thread pool
                    //CompressWithThreadPool(files);
                }
            }
        }

        static void CompressWithThreadPool(string[] threadData)
        {
            int workerItems = Math.Min(GetCoreCount() * 2, threadData.Length);

            ManualResetEvent[] doneEvents = new ManualResetEvent[workerItems];

            for (int i = 0; i < workerItems; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);

                ThreadPool.QueueUserWorkItem(CompressFile, threadData);
            }

            WaitHandle.WaitAll(doneEvents.Where(p => p != null)?.ToArray());
        }

        static void CompressFile(string file)
        {
            if (file == String.Empty)
                return;

            using (ZipArchive compressStream = ZipFile.Open($"{file}.zip", ZipArchiveMode.Update))
            {
                FileInfo fileInfo = new FileInfo(file);
                ZipArchiveEntry currentEntry = compressStream.CreateEntryFromFile(file, fileInfo.Name);

                Console.WriteLine($"File {file} compressed into {file}.zip");
            }
        }


        static void CompressFile(object threadData)
        {
            if (!threadData.Equals(null))
            {
                foreach (string file in (string[])threadData)
                {
                    CompressFile(file);
                }
            }
        }

        static int GetCoreCount()
        {
            int coreCount = 0;

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }

            return coreCount;
        }
    }
}
