[System.Serializable]
public class BehaviourValueInfo
{
    public string BehaviourName { get; private set; }
    public int Level { get; private set; }
    public int MaxLevel { get; private set; }
    public int CurrentExp { get; private set; }
    public int[] MaxExps { get; private set; }
    public float[] BaseValues { get; private set; }
    public float[] CoolTimes { get; private set; }
    public float ValuePerLevel { get; private set; }
}