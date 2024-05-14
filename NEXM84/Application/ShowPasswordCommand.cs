using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using NEXM84.Exception;
using NEXM84.Infrastructure;
using NEXM84.Model;
using NEXM84.UserInterface;
using NEXM84.Utility;

using ICommand = NEXM84.Infrastructure.ICommand;

namespace NEXM84.Application
{
    internal class ShowPasswordCommand : ICommand
    {
        public string Name => "show";

        public string Description => "[]->show the content of the loaded file  |  [site_name]-> Shows a password from the loaded file.";

        PasswordManager PasswordManager;

        public void execute(IUiController iUiController, string[] args)
        {
            PasswordManager = new PasswordManager();
            List<Credential> credentialList = iUiController.GetFileBrowser().getCredentialsFromObservePool();

            if (args.Length < 2) {
                iUiController.writeLine("");
                foreach (Credential credential in credentialList)
                {
                    iUiController.writeLine(" | " + $"Site: {credential.siteName}, Email: {credential.email}, Password: {credential.password}, Date: {credential.addDate}");
                }
            }
            else
            {
                var searchResult = from cred in credentialList
                                   where cred.siteName == args[1]
                                   select cred;

                ShowPassword(iUiController, searchResult.ToList()[0]);
            }
        }

        private void ShowPassword(IUiController uiController, Credential credential)
        {
            string dectriptedPassword = PasswordManager.DecryptPassword(credential.password);

            uiController.writeLine("Site/App: "+credential.siteName +"Email/Username: "+ credential.email +"Password: "+ dectriptedPassword);
        }
    }
}
