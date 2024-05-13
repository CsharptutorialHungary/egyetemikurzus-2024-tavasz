using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF5W4R.Services
{
    public interface ICommandHandler
    {
        void HandleCommand(string command);
    }
}
