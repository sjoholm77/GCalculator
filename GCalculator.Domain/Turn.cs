using System.Collections.Generic;

namespace GCalculator.Domain
{
    public class Turn
    {
        public List<string> Draws { get; set; }
        public List<Play> Plays{ get; set; }
    }
}