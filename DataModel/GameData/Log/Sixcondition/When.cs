[System.Serializable]
public class When
{
    public string Name { get; private set; }
    public string[] Times { get; private set; }
    
    public When(string name, string[] times)
    {
        Name = name;
        Times = times;
    }
}
