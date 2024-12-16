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

        public Classification CheckTradeClassification(DateTime referenceDate)
        {

            if (IsExpired(referenceDate))
                return Classification.EXPIRED;
            if (IsHighRisk())
                return Classification.HIGHRISK;
            if (IsMediumRisk())
                return Classification.MEDIUMRISK;
            else
                return Classification.INVALID;
        }

        private bool IsExpired(DateTime referenceDate) => (NextPaymentDate > referenceDate);
        private bool IsHighRisk() => (Value > 1000000 && ClientSector == "PRIVATE");
        private bool IsMediumRisk() => (Value > 1000000 && ClientSector == "PUBLIC");
    }
        
    public enum Classification
    {
        EXPIRED,
        HIGHRISK,
        MEDIUMRISK,
        INVALID
    }

    public enum ValidClientSector
    {
        PUBLIC,
        PRIVATE
    }
}
