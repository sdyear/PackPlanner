using System.Collections.Generic;

internal class Program
{
    enum SortOrder
    {
        NATURAL,
        LONG_TO_SHORT,
        SHORT_TO_LONG
    }

    class Item
    {
        public Item(int id, int length, int quantity, float weight)
        {
            this.Id = id;
            this.Length = length;
            this.Quantity = quantity;
            this.Weight = weight;
        }
        public int Id { get; set; }
        public int Length { get; set; }
        public int Quantity { get; set; }
        public float Weight { get; set; }
    }
    private static void Main(string[] args)
    {
        string? configString = Console.ReadLine();
        if (string.IsNullOrEmpty(configString)) throw new ArgumentException("config line required");
        string[] configArray = configString.Split(',');
        if (configArray.Length != 3) throw new ArgumentException("4 config arguments are required");

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

        string? itemString;
        List<Item> allItems = new List<Item>();
        while (!string.IsNullOrEmpty(itemString = Console.ReadLine()))
        {
            string[] itemArray = itemString.Split(',');
            if (itemArray.Length != 4) throw new ArgumentException("items must have 4 attribute values");
            Item newItem;
            try
            {
                newItem = new Item(int.Parse(itemArray[0]), int.Parse(itemArray[1]), int.Parse(itemArray[2]), float.Parse(itemArray[3]));
                allItems.Add(newItem);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse item: '{itemString}'");
                return;
            }
        }
        if (order == SortOrder.SHORT_TO_LONG)
            allItems.Sort((i1, i2) => i1.Length.CompareTo(i2.Length));
        else if (order == SortOrder.LONG_TO_SHORT)
            allItems.Sort((i1, i2) => i2.Length.CompareTo(i1.Length));


    }
}