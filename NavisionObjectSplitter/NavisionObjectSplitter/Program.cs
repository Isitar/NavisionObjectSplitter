using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NavisionObjectSplitter
{
    class Program
    {
        private static readonly string MATCHSTRING = @"(OBJECT \w* \d* .*
{)"; //[A-z 0-9]
        static void Main(string[] args)
        {
            Console.Write("Filename: ");
            var fileName = Console.ReadLine();


            using (var sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                var fullFileContent = sr.ReadToEnd();
                var values = Regex.Split(fullFileContent, MATCHSTRING)
                    .AsEnumerable()
                    .Skip(1).ToArray();

                for (int i = 0; i < values.Length; i += 2)
                {
                    var name = values[i].Split(Environment.NewLine.ToCharArray())[0].Trim(new char[] { ' ', '"' });
                    File.WriteAllText(name.Split(' ').Take(3).Aggregate((x, y) => x + " " + y) + ".txt", values[i] + values[i + 1]);
                    Console.WriteLine(name);
                };
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
