using Filemanager.Infrastructure;

namespace Filemanager{
    internal class UI{
        private  readonly CommandProvider _commandProvider;
        private readonly Host _host;

        public UI(CommandProvider commandProvider, Host host){
            _commandProvider = commandProvider;
            _host = host;
            
        }
    }
}