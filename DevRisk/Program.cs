using DevRisk;
using System.Globalization;

List<Trade> Trades = [];
DateTime? ReferenceDate = null;
int? NumberOfTrades = null;

Console.WriteLine("DevRisk");
Console.WriteLine(string.Empty);

while (!InputValidReferenceDate())
{
    Console.WriteLine($"Invalid date, try again!");
    Console.WriteLine();
}

while (!InputValidNumberOfTrades())
{
    Console.WriteLine($"Invalid number of trade, try again!");
    Console.WriteLine();
}

for (int i = 1; i <= NumberOfTrades; i++)
{
    while (!InputTrades(i))
    {
        Console.WriteLine($"Invalid trade, try again!");
    }
}

Console.WriteLine();
Console.WriteLine("OUTPUT");
foreach (var trade in Trades)
{
    Console.WriteLine(trade.CheckTradeCategory(ReferenceDate.Value));
}

#region Inputs
bool InputTrades(int lineNumber)
{

    Console.WriteLine($"Input the trade number {lineNumber} in the portfolio using this pattern below (amount, sector, date):");
    Console.WriteLine("00000000 xxxxxxxxxxxx mm/dd/yyyy");
    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
    {
        Console.WriteLine("Empty value!");
        return false;
    }
    var properties = line.Split(' ');

    if (properties.Length < 3)
    {
        Console.WriteLine("Invalid line!");
        return false;
    }

    var amountConverted = double.TryParse(properties[0], out var price);
    var pendingPaymentConverted = DateTime.TryParse(properties[2], CultureInfo.CreateSpecificCulture("en-US"), out var paymentDate);
    var clientSectorConverted = Enum.TryParse(properties[1].ToUpper(), out ValidClientSector clientSector);

    if (!amountConverted)
    {
        Console.WriteLine("Invalid amount!");
        return false;
    }

    if (!clientSectorConverted)
    {
        Console.WriteLine("Invalid Client Sector");
        return false;
    }

    if (!pendingPaymentConverted)
    {
        Console.WriteLine("Invalid payment date!");
        return false;
    }

    var trade = new Trade(price, clientSector, paymentDate);
    if (trade.CheckTradeCategory(ReferenceDate.Value) == null)
    {
        Trades.Add(trade);
        Console.WriteLine("Trade added!");
        return true;
    }
    else
    {
        Console.WriteLine("Invalid category!");
        return false;
    }

}

bool InputValidNumberOfTrades()
{
    Console.WriteLine("Input the number of trades in the portfolio:");
    var numberTrades = Console.ReadLine();
    var converted = Int32.TryParse(numberTrades, out int validNumberTrades);
    if (converted)
    {
        Console.WriteLine($"Number of Trades added: {validNumberTrades}");
        NumberOfTrades = validNumberTrades;
        return true;
    }
    else
    {
        return false;
    }
}

bool InputValidReferenceDate()
{
    Console.WriteLine("Input the reference date using this pattern (mm/dd/yyyy):");
    var refDate = Console.ReadLine();

    var converted = DateTime.TryParse(refDate, CultureInfo.CreateSpecificCulture("en-US"), out DateTime validDate);

    if (converted)
    {
        Console.WriteLine($"Reference Date added: {validDate.ToString(CultureInfo.CreateSpecificCulture("en-US"))}");
        ReferenceDate = validDate;
        return true;
    }
    else
    {
        return false;
    }

}
#endregion