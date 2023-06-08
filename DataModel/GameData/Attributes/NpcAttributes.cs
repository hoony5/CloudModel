[System.Serializable]
public class NPCAttributes
{
    public string Name { get; private set; }
    public ElementalType ElementalType { get; private set; }
    public string Race { get; private set; }
    public Grade Grade { get; private set; }
    public bool IsBoss { get; private set; }
    public bool IsElite { get; private set; }
    public string[] AttackSkills { get; private set; }
    public string[] DefenseSkills { get; private set; }
    public string[] UtilitySkills { get; private set; }
    public string[] PassiveSkills { get; private set; }
    public string[] MotivationSkills { get; private set; }
    public string[] Places { get; private set; }
    public string Description { get; private set; }
    
    public NPCAttributes(string name, ElementalType elementalType, string race, Grade grade, bool isBoss, bool isElite,
        string[] attackSkills, string[] defenseSkills, string[] utilitySkills, string[] passiveSkills, string[] motivationSkills,
        string[] places, string description)
    {
        Name = name;
        ElementalType = elementalType;
        Race = race;
        Grade = grade;
        IsBoss = isBoss;
        IsElite = isElite;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
        Places = places;
        Description = description;
    }
}