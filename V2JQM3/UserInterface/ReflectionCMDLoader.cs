using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V2JQM3.Infrastructure;

namespace V2JQM3.UserInterface
{
    internal class ReflectionCMDLoader:ICmdProvider
    {
        public IMyCmd[] Commands { get; }

        public ReflectionCMDLoader()
        {
            Type t = typeof(ReflectionCMDLoader);
            var commands = new List<IMyCmd>();
            foreach (var type in t.Assembly.GetTypes())
            {
                if (!type.IsAbstract
                    && !type.IsInterface
                    && type.IsAssignableTo(typeof(IMyCmd)))
                {
                    object? instance = Activator.CreateInstance(type);
                    if (instance is IMyCmd command)
                    {
                        commands.Add(command);
                    }
                }
            }
            Commands = commands.ToArray();
        }
    }
}
