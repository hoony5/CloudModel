[System.Serializable]
public class PetAttributes
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public ElementalType ElementalType { get; private set; }
    public string Race { get; private set; }
    public Grade Grade { get; private set; }
    public string[] AttackSkills { get; private set; }
    public string[] DefenseSkills { get; private set; }
    public string[] UtilitySkills { get; private set; }
    public string[] PassiveSkills { get; private set; }
    public string[] MotivationSkills { get; private set; }
    public string Description { get; private set; }

    public PetAttributes(string name,string type, ElementalType elementalType, string race,Grade grade, string[] attackSkills,
        string[] defenseSkills, string[] utilitySkills, string[] passiveSkills, string[] motivationSkills,
        string description)
    {
        Name = name;
        Type = type;
        ElementalType = elementalType;
        Race = race;
        Grade = grade;
        AttackSkills = attackSkills;
        DefenseSkills = defenseSkills;
        UtilitySkills = utilitySkills;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
        Description = description;
    }

}