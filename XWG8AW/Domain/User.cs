using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XWG8AW.Domain
{
    internal class User
    {
        public User() { }

        public User(string username) { }

        public User(string userName, int score)
        {
            this.UserName = userName;
            this.Score = score;
        }

        public string UserName { get; set; }

        public int Score { get; set; }

        public override string ToString()
        {
            return "Jatekos nev: " + $"{UserName}" + " Eredmenye: " + $"{Score}";
        }
    }
}
