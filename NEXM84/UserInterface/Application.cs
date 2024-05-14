using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

using NEXM84.Application;
using NEXM84.Exception;
using NEXM84.Infrastructure;

namespace NEXM84.UserInterface
{
    internal class Application
    { 
        private readonly UiController _uiController;

        public Application( UiController uiController )
        {
            _uiController = uiController;
        }

        public void Launch()
        {
            _uiController.writeLine($"CLR Version: {Environment.Version}");
            _uiController.writeLine("Hi! Type [help] for further instructions!");
            while (true)
            {
                IUiController.DrawWaitTag();
                string[] input = _uiController.readLine().Split(' ');
                ICommand? command = _uiController.findCommandName(input[0]);
                try {
                    command.execute(_uiController, input);
                }
                catch (NullReferenceException nullReferenceException)
                {
                    _uiController.writeLine("There is no such command " + "\""+ input[0] +"\"!\n Type [help] for help!");
                    Trace.WriteLine(nullReferenceException, "invalid command exception");
                }
                catch(MissingArgException missingArgException)
                {
                    _uiController.writeLine(missingArgException.Message);
                    Trace.WriteLine(missingArgException, "invalid args exception");
                }catch(IOException ioexception)
                {
                    _uiController.writeLine(ioexception.Message);
                    Trace.WriteLine(ioexception, "invalid args exception");
                }
            }
        }
    }
}
