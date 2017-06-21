using System.Collections;
using System.Collections.Generic;

namespace GCalculator.Domain
{
    public class ImportedGame
    {
        public ImportedGame()
        {
            Deck = new List<CardInstance>();
        }
        public List<CardInstance> Deck { get; set; }
        public List<Turn> Turns { get; set; }
    }
}