using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BongoChatFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var matches = new List<Tuple<string, string>>();
            var matchTerms = new List<string>
            {
                "bongo",
                "bingo",
                "bango",
                "congo"
            };

            //remove this if you want to provide a cmd line argument for filename
            args = new[]
            {
                //specify filename
                @"618603627.txt"
            };
            //

            if (args.Length != 1)
            {
                Console.WriteLine("Oi, dickhead... you have to give a filename to parse.");
                Console.WriteLine("Example: BongoChatFinder.exe filename.txt");
                return;
            }
            var filename = args[0];
            Console.WriteLine($"You gave {filename} as the file name");

            var fileContents = File.ReadAllLines(filename);
            foreach (var line in fileContents)
            {
                var timestamp = line.Substring(1, 12); //15
                var content = line.Substring(15).Split(':');
                var message = string.Join(":", content.Skip(1));
                
                if (matchTerms.Any(m => message.ToLower().Contains(m)))
                {
                    Console.WriteLine($"Found Match! at {timestamp} in message {message}");
                    matches.Add(new Tuple<string, string>(timestamp, message));
                }
            }
            var outFilename = filename.Substring(0, filename.Length - (Path.GetExtension(filename).Length)) + "_output.txt";
            File.WriteAllLines(outFilename, matches.Select(m => $"[{m.Item1}]: {m.Item2}"));

        }
    }
}

//BongoChatFinder.exe filename.txt