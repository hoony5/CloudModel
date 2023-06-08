[System.Serializable]
public class StatusItemAttributes
{
    public string Name { get; private set; }
    public ElementalType ElementalType { get; private set; }
    public string Category { get; private set; }
    public Grade Grade { get; private set; }
    public bool IsQuestItem{ get; private set; }
     public int MaxCount { get; private set; }
    public string[] PassiveSkills { get; private set; }
    public string[] Places { get; private set; }
    public string Description { get; private set; }

    public StatusItemAttributes(string name, ElementalType elementalType, string category, Grade grade,
        int maxCount, bool isQuestItem,
        string[] passiveSkills, string[] places,
        string description)
    {
        Name = name;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        MaxCount = maxCount;
        IsQuestItem = isQuestItem;
        PassiveSkills = passiveSkills;
        Places = places;
        Description = description;
    }
}