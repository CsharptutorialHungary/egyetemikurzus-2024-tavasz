using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Exception;
using NEXM84.Infrastructure;
using NEXM84.Model;
using NEXM84.Utility;

namespace NEXM84.Application
{
    internal class AddCommand : ICommand
    {
        public string Name => "add";

        public string Description => "Add a new siteCredential to the pool.";

        private PasswordManager passwordManager;

        public void execute(IUiController iUiController, string[] args)
        {
            passwordManager = new PasswordManager();
            Credential newCredential =  readCredentials(iUiController);
            
            if (newCredential != null )
            {
                iUiController.GetFileBrowser().getCredentialsFromSavePool().Add(newCredential);
                iUiController.writeLine("Added:\n"+newCredential.ToString());
            }
        }

        private Credential readCredentials(IUiController iUiController)
        {                          //[     0     ]    [         1          ]    [   2   ]
            string[] inputTypes = { "site/app name", "email address/username", "password" };
            string[] resultSet = new string[3];
            iUiController.writeLine("------Adding new Credentials. Type [cancel] to abort.------");
            for (int i = 0; i < 3;  i++)
            {
                iUiController.writeLine("Please, provide the "+inputTypes[i]+":");
                IUiController.DrawInputTag();

                if(i == 2) // resultSet[2]/inputTypes[2] is the password
                {
                    resultSet[i] = iUiController.ReadPassword();
                } else
                {
                    resultSet[i] = iUiController.readLine();
                }

                if (resultSet[i].Equals("cancel", StringComparison.CurrentCultureIgnoreCase)){
                    iUiController.writeLine("Canceled.\n");
                    return null;
                }

                if (resultSet[i] == "")
                {
                    Console.WriteLine("------You forget to add the " + inputTypes[i] + "!------");
                    i--; //restarts the current task
                }
            }

            return new Credential
            {
                siteName = resultSet[0],
                email = resultSet[1],
                password = this.passwordManager.AESEncryptPassword(resultSet[2]),
                addDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };
        }

        
    }
}
