using Filemanager.Infrastructure;
using Filemanager.Model;

namespace Filemanager{
    internal class UI{
        private  readonly ICommandProvider _commandProvider;
        private readonly IHost _host;

        private readonly Cache _cache;

        public UI(ICommandProvider commandProvider, IHost host){
            _commandProvider = commandProvider;
            _host = host;
            _cache = new Cache();
            
        }

        public async Task RunAsync(){
            _host.WriteLine("FileManager is running.");
            while(true){
                _host.WriteLine("> ");
                string[] input = _host.ReadLine().Split(" ");
                ICommand? commandToExecute = FindCommandByName(input[0]);
                if(commandToExecute != null){
                    try{
                        await commandToExecute.ExecuteAsync(_host,input,_cache);
                    } catch (Exception exception){
                        _host.WriteLine("Exception occured while executing command "+commandToExecute.Name+": "+exception.Message);
                    }
                    
                } else {
                    _host.WriteLine("No such command");
                }
            }
        }

        private ICommand? FindCommandByName(string name){
            foreach (ICommand command in _commandProvider.Commands){
                if(command.Name.Equals(name)){
                    return command;
                }
            }
            return null;
        }
    }
}