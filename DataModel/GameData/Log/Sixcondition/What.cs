[System.Serializable]
public class What
{
    public string Name { get; private set; }
    public string[] Targets { get; private set; }
    
    public What(string name, string[] targets)
    {
        Name = name;
        Targets = targets;
    }
}
