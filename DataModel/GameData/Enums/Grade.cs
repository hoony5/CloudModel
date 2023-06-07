public enum Grade
{
    None,
    Common,
    Uncommon,
    Rare,
    Unique,
    Legendary,
    Myth,
    Profession
}

public static class GradeUtility
{
    public static Grade StringToGrade(string str)
    {
        return str switch
        {
            "Common" => Grade.Common,
            "Uncommon" => Grade.Uncommon,
            "Rare" => Grade.Rare,
            "Unique" => Grade.Unique,
            "Legendary" => Grade.Legendary,
            "Myth" => Grade.Myth,
            "Profession" => Grade.Profession,
            _ => Grade.None
        };
    }
    public static string GradeToString(this Grade grade)
    {
        return grade switch
        {
            Grade.Common => "Common",
            Grade.Uncommon => "Uncommon",
            Grade.Rare => "Rare",
            Grade.Unique => "Unique",
            Grade.Legendary => "Legendary",
            Grade.Myth => "Myth",
            Grade.Profession => "Profession",
            _ => ""
        };
    }
}