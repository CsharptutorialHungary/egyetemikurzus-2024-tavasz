using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using NEXM84.Infrastructure;
using NEXM84.Model;
using NEXM84.Utility;

namespace NEXM84.Application
{
    internal class SaveToJsonCommand : ICommand
    {
        public string Name => "save";

        public string Description => "Saves the credentials to the currently selected folder.";

        public void execute(IUiController iUiController, string[] args)
        {
            List<Credential> credentials = iUiController.GetFileBrowser().getCredentialsFromSavePool();

            String saveLocation = iUiController.GetFileBrowser().getCurrentDirectory();
            SaveToJson.serialize(credentials, saveLocation);
            iUiController.writeLine("Start saving process...");
        }
    }
}
