using System.Collections.Generic;

internal class Program
{    enum SortOrder
    {
        NATURAL,
        LONG_TO_SHORT,
        SHORT_TO_LONG
    }

    private static void Main(string[] args)
    {
        string? configString = Console.ReadLine();
        if (string.IsNullOrEmpty(configString)) throw new ArgumentException("config line required");
        string[] configArray = configString.Split(',');
        if(configArray.Length != 3) throw new ArgumentException("4 config arguments are required");

        SortOrder order = SortOrder.NATURAL;
        int maxPieces = 0;
        float maxWeight = 0;
        try
        {
            order = (SortOrder)Enum.Parse(typeof(SortOrder), configArray[0]);
            maxPieces = int.Parse(configArray[1]);
            maxWeight = float.Parse(configArray[2]);
        }
        catch (Exception)
        {
            Console.WriteLine($"Unable to parse config: '{configString}'");
            return;
        }

        Console.WriteLine(order.ToString() + ',' + maxPieces + ',' + maxWeight);
    }
}