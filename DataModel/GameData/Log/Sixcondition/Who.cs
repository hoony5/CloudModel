[System.Serializable]
public class Who
{
    public string Name { get; private set; }
    public string[] Subjects { get; private set; }
    public string[] MinSubjectCount { get; private set; }
    public string[] MaxSubjectCount { get; private set; }
    
    public Who(string name, string[] subjects, string[] minSubjectCount, string[] maxSubjectCount)
    {
        Name = name;
        Subjects = subjects;
        MinSubjectCount = minSubjectCount;
        MaxSubjectCount = maxSubjectCount;
    }
}
