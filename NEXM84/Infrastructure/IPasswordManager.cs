using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXM84.Infrastructure
{
    internal interface IPasswordManager
    {
        public string AESEncryptPassword(string password);

        public string DecryptPassword(string encryptedPassword);

    }
}
