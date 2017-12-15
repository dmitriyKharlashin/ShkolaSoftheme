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
        static readonly int maxThreadCount = GetCoreCount() * 2;
        private static long threadCount = 0;
        private static EventWaitHandle eventWaitHandle;
        private static EventWaitHandle clearCount = new EventWaitHandle(false, EventResetMode.AutoReset);
        static object locker = new object();

        static void Main(string[] args)
        {
            while (true)
            {
                //eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

                Console.WriteLine("Enter the folder address to compress it (to exit enter: \"-q\"):");
                string folderAddress = $@"{Console.ReadLine()}";

                if (folderAddress != string.Empty && folderAddress.Equals("-q"))
                {
                    break;
                }

                Stopwatch watch = new Stopwatch();
                watch.Restart();

                lock (locker)
                {
                    Compress(folderAddress);
                }

                //while (Interlocked.Read(ref threadCount) > 0)
                //{
                //    WaitHandle.SignalAndWait(eventWaitHandle, clearCount);
                //}
                Console.WriteLine(watch.ElapsedMilliseconds);
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
                    //Thread thread = new Thread(new ParameterizedThreadStart(CompressFile));
                    //thread.Start(files);

                    // compress with thread pool
                    CompressWithThreadPool(files);
                }
            }
        }

        static void CompressWithThreadPool(string[] threadData)
        {
            ThreadPool.QueueUserWorkItem(CompressFile, threadData);

        }

        static void CompressFile(string file)
        {
            if (file == String.Empty || File.Exists($"{file}.zip"))
                return;

            using (ZipArchive compressStream = ZipFile.Open($"{file}.zip", ZipArchiveMode.Create))
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
                //Interlocked.Increment(ref threadCount);
                //eventWaitHandle.WaitOne();

                foreach (string file in (string[])threadData)
                {
                    CompressFile(file);
                }

                //Interlocked.Decrement(ref threadCount);
                //clearCount.Set();
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
