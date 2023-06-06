[System.Serializable]
public class ExpenseAbilityStat
{
    public bool IsContinuousCost { get; private set; }
    public string ReflectCurrentStatName { get; private set; }
    public string ReflectMaxStatName { get; private set; }
    public float Cost { get; private set; }
    // role instead of Index, if send to server level return cost
    public int CurrentLevel { get; private set; }
    // role instead of Max Index
    public int MaxLevel { get; private set; }
    public DataUnitType DataUnitType { get; private set; }
    public CalculationType CalculationType { get; private set; }
}
