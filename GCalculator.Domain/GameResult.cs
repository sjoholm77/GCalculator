namespace GCalculator.Domain
{
    public class GameResult
    {
        public TurnResult FirstTurn { get; set; }
        public TurnResult SecondTurnResult { get; set; }
        public TurnResult ThirdTurn { get; set; }
    }
}