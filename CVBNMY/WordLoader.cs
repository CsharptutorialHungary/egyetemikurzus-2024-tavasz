using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBNMY
{
    internal static class WordLoader
    {
        private const string WORD_FILES_PATH =          @"\..\..\..\WordFiles\";
        private const string EASY_WORD_FILE_PATH =      "EasyWords.txt";
        private const string MEDIUM_WORD_FILE_PATH =    "MediumWords.txt";
        private const string HARD_WORD_FILE_PATH =      "HardWords.txt";
        public static string[]? ReadWords(string filePath)
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

        public static string WordFilePath(int difficulty)
        {
            return CreateWordFilePath(difficulty);
        }
        private static string CreateWordFilePath(int difficulty)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, @"..\..\..\WordFiles\");
            switch (difficulty)
            {
                case 0:
                {
                    sFile = Path.Combine(sFile, EASY_WORD_FILE_PATH);
                    break;
                }
                case 1:
                {
                        sFile = Path.Combine(sFile, MEDIUM_WORD_FILE_PATH);
                        break;
                }
                case 2:
                {
                        sFile = Path.Combine(sFile, HARD_WORD_FILE_PATH);
                        break;
                }
            }
            return Path.GetFullPath(sFile);
        }
    }

  
}
