using WHBNDL.Domain;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class ExitCommand : IShellCommand
    {
        public string Name => "exit";

        public void Execute(IHost host, string[] args)
        {
            var textsDeserializer = new AppTextsDeserializer();
            Texts constTexts = AppTextsDeserializer.Deserialize();
            host.WriteLine(constTexts.End);
            host.Exit();
        }
    }
}
