[System.Serializable]
public class MotivationStatusInfo
{
    public bool MotivationActive { get; private set; }
    public bool HasReflectMyStatus { get; private set; }
    public bool HasReflectMaxStatus { get; private set; }
    public string CurrentStatName { get; private set; }
    public string MaxStatName { get; private set; }
    public float ReflectValue { get; private set; }
    public DataUnitType ReflectValueUnitType { get; private set; }
    public string MotivatedStatName { get; private set; }
    public float MotivatedValue { get; private set; }
    public DataUnitType MotivatedValueUnitType { get; private set; }
    public ComparerType MotivationComparerType { get; private set; }
    public CalculationType CalculationType { get; private set; }
    public ApplyTargetType ApplyTargetType { get; private set; }
    public float AddedValue { get; private set; }
    public float MultipliedValue { get; private set; }
    public float PreviousValue { get; private set; }
}