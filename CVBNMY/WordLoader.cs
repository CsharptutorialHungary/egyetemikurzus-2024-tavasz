using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal class WordLoader
    {
        public string[]? ReadWords(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }

            string[] words;
            try
            {
                words = File.ReadAllLines(filePath);

                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Unable to access file");
                words = null;
            }
            catch(IOException) 
            {
                Console.WriteLine("I/O exception occured while reading the file");
                words = null;

            }
            return words;
        }
    }
}
