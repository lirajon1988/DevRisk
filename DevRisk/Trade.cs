namespace DevRisk
{
    public class Trade : ITrade
    {
        public double Value { get; }
        public string ClientSector { get; }
        public DateTime NextPaymentDate { get; }

        public Trade(double amount, ValidClientSector clientSector, DateTime nextPendingDate)
        {
            Value = amount;
            ClientSector = clientSector.ToString();
            NextPaymentDate = nextPendingDate;
        }

        public Category? CheckTradeCategory(DateTime referenceDate)
        {

            if (IsExpired(referenceDate))
                return Category.EXPIRED;
            if (IsHighRisk())
                return Category.HIGHRISK;
            if (IsMediumRisk())
                return Category.MEDIUMRISK;
            else
                return null;
        }

        private bool IsExpired(DateTime referenceDate) => (NextPaymentDate > referenceDate);
        private bool IsHighRisk() => (Value > 1000000 && ClientSector == "PRIVATE");
        private bool IsMediumRisk() => (Value > 1000000 && ClientSector == "PUBLIC");
    }
        
    public enum Category
    {
        EXPIRED,
        HIGHRISK,
        MEDIUMRISK
    }

    public enum ValidClientSector
    {
        PUBLIC,
        PRIVATE
    }
}
