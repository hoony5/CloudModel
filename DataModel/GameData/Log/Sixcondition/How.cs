[System.Serializable]
public class How
{
    public string Name { get; private set; }
    public string[] CompletionCounts { get; private set; }
    public string[] Means { get; private set; }
    public string[] Behaviours { get; private set; }
    
    public How(string name,string[] means, string[] completionCounts, string[] behaviours)
    {
        Name = name;
        Means = means;
        CompletionCounts = completionCounts;
        Behaviours = behaviours;
    }
}
