using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Exception;
using NEXM84.Infrastructure;
using NEXM84.Model;
using NEXM84.UserInterface;
using NEXM84.Utility;

namespace NEXM84.Application
{
    internal class LoadFile : ICommand
    {
        public string Name => "load";

        public string Description => "[file_name]-> Loads in a json file.";

        public void execute(IUiController iUiController, string[] args)
        {
            if (args.Length == 1) throw new MissingArgException(this.Name);           
            string currentFolder = iUiController.GetFileBrowser().getCurrentDirectory();
            string fileName = args[1];
            string extension = Path.GetExtension(fileName);
            if (!isJson(extension)) throw new IOException();
            string filePath = Path.Combine(currentFolder, fileName);
            iUiController.GetFileBrowser().setCurrentFile(filePath);
            iUiController.writeLine("Selected: "+filePath);
            LoadFromJson.LoadCredentialsFromJson(filePath, iUiController);
        }

        private Boolean isJson(string fileSuffix)
        {
            return fileSuffix.Equals(".json") ? true : false;
        }

        

    }
}
