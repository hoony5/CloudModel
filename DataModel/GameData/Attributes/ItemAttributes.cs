[System.Serializable]
public class ItemAttributes
{
    public string Name { get; private set; }
    public ElementalType ElementalType { get; private set; }
    public string Category { get; private set; }
    public Grade Grade { get; private set; }
     public bool IsQuestItem { get; private set; }
     public int MaxCount { get; private set; }
    public string Description { get; private set; }
    
    public ItemAttributes(string name, ElementalType elementalType, string category, Grade grade, int maxCount,bool isQuestItem, string description)
    {
        Name = name;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        IsQuestItem = isQuestItem;
        MaxCount = maxCount;
        Description = description;
    }
}