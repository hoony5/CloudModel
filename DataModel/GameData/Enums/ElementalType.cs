public enum ElementalType
{
    Normal,
    Water,
    Fire,
    Plant,
    Earth,
    Metal,
    Lightning,
    Shadow,
    Wind,
    Ice,
    Lava,
    Light,
    Dark,
    Forest,
    Spirit,
}

public static class ElementTypeUtility
{
    public static string ElementTypeToString(this ElementalType elementalType)
    {
        return elementalType switch
        {
            ElementalType.Water => "Water",
            ElementalType.Fire => "Fire",
            ElementalType.Plant => "Plant",
            ElementalType.Earth => "Earth",
            ElementalType.Metal => "Metal",
            ElementalType.Lightning => "Lightning",
            ElementalType.Shadow => "Shadow",
            ElementalType.Wind => "Wind",
            ElementalType.Ice => "Ice",
            ElementalType.Lava => "Lava",
            ElementalType.Light => "Light",
            ElementalType.Dark => "Dark",
            ElementalType.Forest => "Forest",
            ElementalType.Spirit => "Spirit",
            _ => "Normal",
        };
    }
    public static ElementalType StringToElementType(string str)
    {
        return str switch
        {
            "Water" => ElementalType.Water,
            "Fire" => ElementalType.Fire,
            "Plant" => ElementalType.Plant,
            "Earth" => ElementalType.Earth,
            "Metal" => ElementalType.Metal,
            "Lightning" => ElementalType.Lightning,
            "Shadow" => ElementalType.Shadow,
            "Wind" => ElementalType.Wind,
            "Ice" => ElementalType.Ice,
            "Lava" => ElementalType.Lava,
            "Light" => ElementalType.Light,
            "Dark" => ElementalType.Dark,
            "Forest" => ElementalType.Forest,
            "Spirit" => ElementalType.Spirit,
            _ => ElementalType.Normal,
        };
    }
}