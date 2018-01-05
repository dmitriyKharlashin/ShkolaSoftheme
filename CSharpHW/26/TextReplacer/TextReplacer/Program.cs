using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace TextReplacer
{
    class Program
    {
        static DirectoryInfo _directory;
        static FileInfo[] _files;
        static string _searchedText;
        static string _replacer;
        static Task[] _tasks;
        static Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory destination for replace text in all txt files:");

            
            while (true)
            {
                string destinationAddress = $@"{Console.ReadLine()}";

                if (String.IsNullOrEmpty(destinationAddress))
                {
                    _directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                }
                else
                {
                    _directory = new DirectoryInfo(destinationAddress);

                    if (!_directory.Exists)
                    {
                        Console.WriteLine("Please, re-enter the correct folder address");
                        continue;
                    }
                }

                _files = _directory.GetFiles("*.txt");
                break;
            }

            Console.WriteLine("Enter the needle text:");
            while (true)
            {
                _searchedText = $"{Console.ReadLine()}";

                if (_searchedText.Equals(String.Empty))
                {
                    Console.WriteLine("Please, re-enter the correct text:");
                    continue;
                }

                break;
            }
            
            Console.WriteLine("Enter the new text value:");
            while (true)
            {
                _replacer = $"{Console.ReadLine()}";

                if (_replacer.Equals(String.Empty))
                {
                    Console.WriteLine("Please, re-enter the correct text:");
                    continue;
                }

                break;
            }

            /*
            _tasks = new Task[_files.Length];
            for (var i = 0; i < _files.Length; i++)
            {
                FileInfo file = _files[i];

                Task task = new Task(() =>
                {
                    ReadAndChangeText(file, _searchedText, _replacer);
                });

                task.Start();

                _tasks.SetValue(task, i);
            }

            Task.WhenAll(_tasks);
            */

            Parallel.ForEach(_files,
                new ParallelOptions(){ MaxDegreeOfParallelism = Environment.ProcessorCount * 2 },
                f =>
                {
                    ReadAndChangeText(f, _searchedText, _replacer);
                });
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
                        
                        _logger.Info($"On line {lineNumber} in file {file.Name} - the searched text {searchedText} was found and changed into {replacer}");
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
