using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Exception;
using NEXM84.Infrastructure;
using NEXM84.UserInterface;

namespace NEXM84.Application
{
    internal class FileBrowserCommand : ICommand
    {
        public string Name => "fb";

        public string Description => "Helps to navigate trought your files\n| Args:\n" +
            "| [fb cd] [folder_name]-> enter folder\n" +
            "| [fb back]-> goes to the previous folder\n" +
            "| [fb cf]-> prints_current_selected_folder\n" +
            "| [fb cd] [desktop; documents; downloads; main]-> Reach common folders.\n" +
            "|";

        string currentDirectory;

        public void execute(IUiController iUiController, string[] args)
        {
            string fileBrowserCommand = getFileBrowserCommand(args);
            this.currentDirectory = iUiController.GetFileBrowser().getCurrentDirectory();
            switch (fileBrowserCommand)
            {
                case "cd":
                    if (args.Length < 3) return;
                    executeCd(iUiController, args[2]);
                    break;
                case "cf":
                    executeCurrentFolder(iUiController);
                    break;
                case "back":
                    backToPrevFolder(iUiController);
                    break;
                default:
                    throw new MissingArgException(this.Name);
                    break;
            }
        }

        private string getFileBrowserCommand(string[] args)
        {
            if (args.Length >= 2)
            {
                return args[1];
            }
            else
            {
                return "empty";
            }
        }

        private void executeCd(IUiController iUiController, string directoryName)
        {
            if (goToSpecialFolder(iUiController, directoryName)) return;
            string newPath = Path.Combine(currentDirectory, directoryName);

            if (Directory.Exists(newPath))
            {
                currentDirectory = newPath;
                iUiController.GetFileBrowser().setCurrentDirectory(newPath);
                iUiController.GetFileBrowser().DisplayFilesAndDirectories();
            }
            else
            {
                iUiController.writeLine("Directory not found.");
            }
        }

        private void executeCurrentFolder(IUiController iUiController)
        {
            iUiController.GetFileBrowser().DisplayFilesAndDirectories();
        }

        private bool goToSpecialFolder(IUiController iUiController, string folderName)
        {
            switch (folderName)
            {
                case "desktop":
                    iUiController.GetFileBrowser().setCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                    break;
                case "documents":
                    iUiController.GetFileBrowser().setCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    break;
                case "downloads":
                    iUiController.GetFileBrowser().setCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                    break;
                case "main":
                    iUiController.GetFileBrowser().setCurrentDirectory(Path.GetPathRoot(Environment.SystemDirectory));
                    break;
                default:
                    return false;
            }
            iUiController.GetFileBrowser().DisplayFilesAndDirectories();
            return true;
        }

        private bool backToPrevFolder(IUiController uiController)
        {
            int lastIndex = currentDirectory.LastIndexOf(Path.DirectorySeparatorChar);
            if (lastIndex >= 0)
            {
                this.currentDirectory =  currentDirectory.Substring(0, lastIndex);
                uiController.GetFileBrowser().setCurrentDirectory(currentDirectory);
                uiController.GetFileBrowser().DisplayFilesAndDirectories();
                return true;
            }
            else
            {
                uiController.writeLine("You cant go back further!");
                return false;
            }
        }
    }
}
