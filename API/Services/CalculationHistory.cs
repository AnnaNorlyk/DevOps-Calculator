namespace Calculator.Services
{
    public class CalculationHistory
    {
        public int Id { get; set; }
        public string Operation { get; set; } = null!;
        public int? OperandA { get; set; }
        public int? OperandB { get; set; }
        public double Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}