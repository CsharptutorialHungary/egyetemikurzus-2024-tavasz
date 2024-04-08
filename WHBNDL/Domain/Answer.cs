using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Domain
{
    public sealed record class Answer
    {
        public required string CorrectAnswer { get; set; }
        public required string[] WrongAnswers  { get; set; }
    }
}
