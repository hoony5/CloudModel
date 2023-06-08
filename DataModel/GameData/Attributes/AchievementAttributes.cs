[System.Serializable]
public class AchievementAttributes
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Category { get; private set; }
    public Grade Grade { get; private set; }
    public string[] PassiveSkills { get; private set; }
    public string[] MotivationSkills { get; private set; }
    public string[] Conditions { get; private set; }
    public string Description { get; private set; }

    public AchievementAttributes(string name,string type, string category, Grade grade,
        string[] passiveSkills, string[] motivationSkills, string[] conditions,
        string description)
    {
        Name = name;
        Type = type;
        Category = category;
        Grade = grade;
        PassiveSkills = passiveSkills;
        MotivationSkills = motivationSkills;
        Conditions = conditions;
        Description = description;
    }
}