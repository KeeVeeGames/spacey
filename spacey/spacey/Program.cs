using System;
using System.IO;
using System.Text.RegularExpressions;

namespace spacey
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDirectory = @"C:\Users\MusNik\Documents\GameMakerStudio2\CloseYourEyes";
            int tabWidth = 4;

            ConvertTabsToSpaces(rootDirectory, tabWidth);

            Console.WriteLine("Conversion completed.");
        }

        static void ConvertTabsToSpaces(string directory, int tabWidth)
        {
            string[] gmlFiles = Directory.GetFiles(directory, "*.gml", SearchOption.AllDirectories);

            foreach (string filePath in gmlFiles)
            {
                string fileContent = File.ReadAllText(filePath);
                string convertedContent = ConvertTabsToSpacesInText(fileContent, tabWidth);
                File.WriteAllText(filePath, convertedContent);
            }
        }

        static string ConvertTabsToSpacesInText(string text, int tabWidth)
        {
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                int tabIndex = line.IndexOf('\t');
                while (tabIndex != -1)
                {
                    int spacesCount = tabWidth - (tabIndex % tabWidth);
                    string spaces = new string(' ', spacesCount);

                    line = line.Remove(tabIndex, 1).Insert(tabIndex, spaces);
                    tabIndex = line.IndexOf('\t', tabIndex + spacesCount);
                }

                lines[i] = line;
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}
