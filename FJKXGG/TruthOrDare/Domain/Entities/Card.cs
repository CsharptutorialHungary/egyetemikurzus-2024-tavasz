using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDare.Domain.Entities;
public record Card(int Id, string Text, GameMode GameMode) : ICard;