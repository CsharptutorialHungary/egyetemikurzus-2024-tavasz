using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHBNDL.Domain
{
    public sealed record class Question
    {
        public required string QuestionText { get; set; }
        public required string GoodAnswerText { get; set; }
        public required string[] BadAnswersArray { get; set; }
    }
}
