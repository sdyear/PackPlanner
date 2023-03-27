using System.Collections.Generic;
using static PackPlanner.Program;

namespace PackPlanner;
public class Program
{
    public enum SortOrder
    {
        NATURAL,
        LONG_TO_SHORT,
        SHORT_TO_LONG
    }

    public class Item
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
    public static void Main(string[] args)
    {
        string? configString = Console.ReadLine();
        if (string.IsNullOrEmpty(configString))
        {
            Console.Error.WriteLine("Configuration line cannot be empty");
            return;
        }
        string[] configArray = configString.Split(',');
        if (configArray.Length != 3)
        {
            Console.Error.WriteLine("Configuration line must have 3 values");
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
            Console.Error.WriteLine($"Unable to parse config: '{configString}'");
            return;
        }
        if (maxPieces < 1)
        {
            Console.Error.WriteLine("Atleast 1 piece must fit in a pack");
            return;
        }

        string? itemString;
        List<Item> allItems = new List<Item>();
        while (!string.IsNullOrEmpty(itemString = Console.ReadLine()))
        {
            string[] itemArray = itemString.Split(',');
            if (itemArray.Length != 4)
            {
                Console.Error.WriteLine("Item line must have 4 values");
                return;
            }
            Item newItem;
            try
            {
                newItem = new Item(int.Parse(itemArray[0]), int.Parse(itemArray[1]), int.Parse(itemArray[2]), float.Parse(itemArray[3]));
                allItems.Add(newItem);
                if (newItem.Weight > maxWeight)
                {
                    Console.Error.WriteLine("Pieces must be equal to or lighter than max pack weight");
                    return;
                }
            }
            catch (FormatException)
            {
                Console.Error.WriteLine($"Unable to parse item: '{itemString}'");
                return;
            }
        }

        List<List<Item>> allPacks = CreatePacks(order, maxPieces, maxWeight, allItems);

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

    /// <summary>
    /// Takes a list of items, sorts them and puts them into packs
    /// that meet the restrictions defined in the arguments.
    /// </summary>
    /// <param name="order">The order the items should be sorted by length</param>
    /// <param name="maxItems">The maximum number of items per pack</param>
    /// <param name="maxWeight">The maximum weight of the items in a pack</param>
    /// <param name="items">The list of the items to be put in packs</param>
    /// <returns></returns>
    public static List<List<Item>> CreatePacks(SortOrder order, int maxItems, float maxWeight, List<Item> items)
    {
        if (order == SortOrder.SHORT_TO_LONG)
            items.Sort((i1, i2) => i1.Length.CompareTo(i2.Length));
        else if (order == SortOrder.LONG_TO_SHORT)
            items.Sort((i1, i2) => i2.Length.CompareTo(i1.Length));

        int currentItems = 0;
        float currentWeight = 0;
        List<Item> currentPack = new List<Item>();
        List<List<Item>> allPacks = new List<List<Item>>();
        foreach (Item item in items)
        {
            for (int i = 0; i < item.Quantity; i++)
            {
                if (currentItems + 1 <= maxItems && currentWeight + item.Weight <= maxWeight)
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
        if (currentPack.Count > 0) allPacks.Add(currentPack);
        return allPacks;
    }
}