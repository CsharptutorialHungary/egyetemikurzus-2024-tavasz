using WHBNDL.Domain;
using WHBNDL.Infrastructure;

namespace WHBNDL.Application
{
    internal class DescriptionCommand : IShellCommand
    {
        public string Name => "description";

        public void Execute(IHost host, string[] args)
        {
            var textsDeserializer = new AppTextsDeserializer();
            Texts constTexts = AppTextsDeserializer.Deserialize();
            host.WriteLine(constTexts.Description);
        }
    }
}
