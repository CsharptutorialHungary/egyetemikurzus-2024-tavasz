using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TruthOrDare.Domain.Entities;

namespace TruthOrDare.Application.Ports
{
    /// <summary>
    /// Outgoing port for card database repository
    /// </summary>
    internal interface ICardRepositoryPort
    {
        /// <summary>
        /// Get all cards from every game mode and card type
        /// </summary>
        /// <returns>List of every available card in every game mode and card type</returns>
        IEnumerable<Card> GetAllCards();
        void GenerateDefaultCards();
        IEnumerable<T> GetCardsByType<T>() where T : Card;
    }
}
