using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Model;

namespace NEXM84.UserInterface
{
    internal sealed class FileBrowser
    {
        string currentDirectory;
        string currentFile;
        private List<Credential> credentialsToSave;
        private List<Credential> credentialsFromSave;
        public FileBrowser()
        {
            this.credentialsToSave = new List<Credential>();
            this.currentDirectory = Directory.GetCurrentDirectory();
            this.currentFile = currentDirectory;
        }

        public void DisplayFilesAndDirectories()
        {
            Console.WriteLine($"Contents of {currentDirectory}:");
            string[] directories = Directory.GetDirectories(currentDirectory);
            foreach (string directory in directories)
            {
                Console.WriteLine($"[DIR] {Path.GetFileName(directory)}");
            }

            string[] files = Directory.GetFiles(currentDirectory);
            foreach (string file in files)
            {
                Console.WriteLine($"      {Path.GetFileName(file)}");
            }
        }

        public string getCurrentDirectory()
        {
            return currentDirectory;
        }

        public void setCurrentDirectory(string newPath)
        {
           this.currentDirectory = newPath;
        }

        public string getCurrentFile()
        {
            return this.currentFile;
        }

        public void setCurrentFile(string file)//TODO
        {
            this.currentFile = file;
        }

        public void addCredentialToSavePool(Credential newCredential)
        {
            this.credentialsToSave.Add(newCredential);
        }

        public List<Credential> getCredentialsFromSavePool()
        {
            return credentialsToSave;
        }

        public void setCredentialToObservePool(List<Credential> credentials)
        {
            this.credentialsFromSave = credentials;
        }

        public List<Credential> getCredentialsFromObservePool()
        {
            return credentialsFromSave;
        }
    }
}
