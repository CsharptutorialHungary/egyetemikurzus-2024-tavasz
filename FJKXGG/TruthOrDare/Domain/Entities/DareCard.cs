using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.Domain.Entities
{
    public sealed record DareCard(int Id, string Text, GameMode GameMode) : Card(Id, Text, GameMode), ICard;
}
