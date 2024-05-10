using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Domain;
using XWG8AW.Infrastructure;

namespace XWG8AW.Application
{
    internal class ListCommand : IShellCommand
    {
        public string Name => "list";

        public void Execute(IHost host, string[] args)
        {

            /*UserDeserializer a = new UserDeserializer();
            a.UserDeserializeFromJson();*/

            /*QuestionDeserializer b = new QuestionDeserializer();
            b.QuestionDeserializeFromJson();*/

            User user = new User("dsa", 99);


            /*UserSerializer c = new UserSerializer();
            c.UseSerializeToJson(user);*/
        }
    }
}
