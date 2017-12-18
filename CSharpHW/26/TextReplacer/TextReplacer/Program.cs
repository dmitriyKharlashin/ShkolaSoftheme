using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextReplacer
{
    class Program
    {
        static DirectoryInfo directory;
        static FileInfo[] files;
        static string searchedText;
        static string replacer;
        static Task[] tasks;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory destination for replace text in all txt files:");

            
            while (true)
            {
                string destinationAddress = $@"{Console.ReadLine()}";
                directory = new DirectoryInfo(destinationAddress);

                if (destinationAddress.Equals(String.Empty) || !directory.Exists)
                {
                    Console.WriteLine("Please, re-enter the correct folder address");
                    continue;
                }

                files = directory.GetFiles("*.txt");
                break;
            }

            Console.WriteLine("Enter the needle text:");
            while (true)
            {
                searchedText = $"{Console.ReadLine()}";

                if (searchedText.Equals(String.Empty))
                {
                    Console.WriteLine("Please, re-enter the correct text:");
                    continue;
                }

                break;
            }
            
            Console.WriteLine("Enter the new text value:");
            while (true)
            {
                replacer = $"{Console.ReadLine()}";

                if (replacer.Equals(String.Empty))
                {
                    Console.WriteLine("Please, re-enter the correct text:");
                    continue;
                }

                break;
            }

            tasks = new Task[files.Length];
            for (var i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                Task task = new Task(() =>
                {
                    ReadAndChangeText(file, searchedText, replacer);
                });

                task.Start();

                tasks.SetValue(task, i);
            }

            Task.WaitAll(tasks);
        }

        static void ReadAndChangeText(FileInfo file, string searchedText, string replacer)
        {
            string newLine = "";
            using (StreamReader streamReader = new StreamReader(file.FullName))
            {
                int lineNumber = 0;
                while (!streamReader.EndOfStream)
                {
                    lineNumber++;
                    string line = streamReader.ReadLine();
                    if (line != null && line.Contains(searchedText))
                    {
                        newLine += line.Replace(searchedText, replacer) + "\n";

                        Console.WriteLine($"On line {lineNumber} the searched text {searchedText} was found and changed into {replacer}");
                        continue;
                    }

                    newLine += line + "\n";
                }
            }

            if (!newLine.Equals(String.Empty))
            {
                using (StreamWriter streamWriter = new StreamWriter(file.FullName))
                {

                    streamWriter.Write(newLine);
                }
            }
        }
    }
}
