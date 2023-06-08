[System.Serializable]
public class EquipmentAttributes
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public ElementalType ElementalType { get; private set; }
    public string Category { get; private set; }
    public Grade Grade { get; private set; }
     public bool IsQuestItem { get; private set; }
     public int MaxCount { get; private set; }
    public string[] AttackSkills { get; private set; }
    public string[] DefenseSkills { get; private set; }
    public string[] UtilitySkills { get; private set; }
    public string[] PassiveSkills { get; private set; }
    public string[] Places { get; private set; }
    public string Description { get; private set; }
    
    public EquipmentAttributes(string name ,string type , ElementalType elementalType, string category, Grade grade,
        int maxCount, bool isQuestItem,
        string[] attackSkills, 
        string[] defenseSkills,
        string[] utilitySkills,
        string[] passiveSkills, 
        string[] places,
        string description)
    {
        Name = name;
        Type = type;
        ElementalType = elementalType;
        Category = category;
        Grade = grade;
        MaxCount = maxCount;
        IsQuestItem = isQuestItem;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        Places = places;
        Description = description;
    }
}