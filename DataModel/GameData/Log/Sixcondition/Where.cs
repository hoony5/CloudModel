[System.Serializable]
public class Where
{
    public string Name { get; private set; }
    public string[] Places { get; private set; }
    
    public Where(string name, string[] places)
    {
        Name = name;
        Places = places;
    }
}
