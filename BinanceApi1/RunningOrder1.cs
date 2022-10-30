namespace BinanceApi1
{
    internal class RunningOrder
    {
        public int Id { set; get; }
        public string pair { set; get; }
        public decimal initialWorth { set; get; }
        public decimal initialRate { set; get; }
    }
}