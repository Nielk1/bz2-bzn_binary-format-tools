namespace BZNParser.Battlezone
{
    public class BattlezoneBZNParseFailException : Exception
    {
        public string PrjID { get; set; }
        public BattlezoneBZNParseFailException(string PrjID, string message) : base(message)
        {
            this.PrjID = PrjID;
        }
        public BattlezoneBZNParseFailException(string PrjID, string message, Exception innerException) : base(message, innerException)
        {
            this.PrjID = PrjID;
        }
    }
}