[System.Serializable]
public class NPCLootInfo
{
    public string Name { get; private set; }
    public int LootExp { get; private set; }
    public int LootMoney { get; private set; }
    public string[] LootCommonItems { get; private set; }
    public float LootCommon { get; private set; }
    public string[] LootUncommonItems { get; private set; }
    public float LootUncommon { get; private set; }
    public string[] LootRareItems { get; private set; }
    public float LootRare { get; private set; }
    public string[] LootUniqueItems { get; private set; }
    public float LootUnique { get; private set; }
    public string[] LootLegendaryItems { get; private set; }
    public float LootLegendary { get; private set; }
    public string[] LootMythItems { get; private set; }
    public float LootMyth { get; private set; }
    
    public NPCLootInfo(string name, int lootExp, int lootMoney, string[] lootCommonItems, float lootCommon, string[] lootUncommonItems, float lootUncommon, string[] lootRareItems, float lootRare, string[] lootUniqueItems, float lootUnique, string[] lootLegendaryItems, float lootLegendary, string[] lootMythItems, float lootMyth)
    {
        Name = name;
        LootExp = lootExp;
        LootMoney = lootMoney;
        LootCommonItems = lootCommonItems;
        LootCommon = lootCommon;
        LootUncommonItems = lootUncommonItems;
        LootUncommon = lootUncommon;
        LootRareItems = lootRareItems;
        LootRare = lootRare;
        LootUniqueItems = lootUniqueItems;
        LootUnique = lootUnique;
        LootLegendaryItems = lootLegendaryItems;
        LootLegendary = lootLegendary;
        LootMythItems = lootMythItems;
        LootMyth = lootMyth;
    }
}
