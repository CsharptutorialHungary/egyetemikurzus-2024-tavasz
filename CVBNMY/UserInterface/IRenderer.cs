using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CVBNMY.Application;

namespace CVBNMY.UserInterface
{
    /// <summary>
    /// This interface is used to provide functions for rendering the console output.
    /// </summary>
    internal interface IRenderer
    {
        public void RenderGameState(string currentHiddenWord, List<char> characterGuesses, int remainingGuesses);
        public void RenderDifficultyOptions(Difficulty selectedDifficulty, string[] optionTexts);
        public void RenderClear();
    }
}
