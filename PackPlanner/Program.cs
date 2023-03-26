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
        if (string.IsNullOrEmpty(configString))
        {
            Console.WriteLine("Configuration line cannot be empty");
            return;
        }
        string[] configArray = configString.Split(',');
        if (configArray.Length != 3)
        {
            Console.WriteLine("Configuration line must have 4 values");
            return;
        }

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
        if (maxPieces < 1)
        {
            Console.WriteLine("Atleast 1 piece must fit in a pack");
            return;
        }

        string? itemString;
        List<Item> allItems = new List<Item>();
        while (!string.IsNullOrEmpty(itemString = Console.ReadLine()))
        {
            string[] itemArray = itemString.Split(',');
            if (itemArray.Length != 4)
            {
                Console.WriteLine("Item line must have 4 values");
                return;
            }
            Item newItem;
            try
            {
                newItem = new Item(int.Parse(itemArray[0]), int.Parse(itemArray[1]), int.Parse(itemArray[2]), float.Parse(itemArray[3]));
                allItems.Add(newItem);
                if (newItem.Weight > maxWeight)
                {
                    Console.WriteLine("piece must be equal to or lighter than max pack weight");
                    return;
                }
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

        int currentItems = 0;
        float currentWeight = 0;
        List<Item> currentPack = new List<Item>();
        List<List<Item>> allPacks = new List<List<Item>>();
        foreach (Item item in allItems)
        {
            for (int i = 0; i < item.Quantity; i++)
            {
                if (currentItems + 1 <= maxPieces && currentWeight + item.Weight <= maxWeight)
                {
                    currentItems++;
                    currentWeight += item.Weight;
                    if (currentPack.Count != 0 && currentPack.Last().Id == item.Id)
                    {
                        currentPack[^1].Quantity++;
                    }
                    else
                    {
                        currentPack.Add(new Item(item.Id, item.Length, 1, item.Weight));
                    }

                }
                else
                {
                    allPacks.Add(currentPack);
                    currentPack = new List<Item>();
                    currentPack.Add(new Item(item.Id, item.Length, 1, item.Weight));
                    currentItems = 1;
                    currentWeight = item.Weight;
                }
            }
        }
        allPacks.Add(currentPack);
        for (int i = 0; i < allPacks.Count; i++)
        {
            Console.WriteLine($"Pack Number: {i + 1}");
            int packLength = 0;
            float packWeight = 0;
            foreach (Item item in allPacks[i])
            {
                Console.WriteLine($"{item.Id}, {item.Length}, {item.Quantity},{item.Weight}");
                if (item.Length > packLength)
                    packLength = item.Length;
                packWeight += item.Quantity * item.Weight;
            }
            Console.WriteLine($"Pack Length: {packLength}, Pack Weight: {packWeight}");
        }
    }
}