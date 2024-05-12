using Filemanager.Infrastructure;

namespace Filemanager{
    internal class UI{
        private  readonly ICommandProvider _commandProvider;
        private readonly IHost _host;

        public UI(ICommandProvider commandProvider, IHost host){
            _commandProvider = commandProvider;
            _host = host;
            
        }

        public void Run(){
            _host.WriteLine("FileManager is running.");
            while(true){
                _host.WriteLine("> ");
                string[] input = _host.ReadLine().Split(" ");
                ICommand? commandToExecute = findCommandByName(input[0]);
                if(commandToExecute != null){
                    commandToExecute.Execute(_host,input);
                } else {
                    _host.WriteLine("No such command");
                }
            }
        }

        private ICommand? findCommandByName(String name){
            foreach (ICommand command in _commandProvider.Commands){
                if(command.Name.Equals(name)){
                    return command;
                }
            }
            return null;
        }
    }
}