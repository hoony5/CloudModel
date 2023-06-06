[System.Serializable]
public class EffectAbilityStat
{
    public string RawName { get; private set; }
    public string DisplayName { get; private set; }
    public float Value { get; private set; }
    public float AddedValue { get; private set; }
    public float MultipliedValue { get; private set; }
    public float PreviousValue { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }
    public ApplyTargetType ApplyTargetType {get; private set;}
    public CalculationType CalculationType {get; private set;}
    public DataUnitType DataUnitType {get; private set;}
    
    public EffectAbilityStat(string statRawName, float value, int min, int max, string calculationType, string applyTargetType, string dataUnitType)
    {
        RawName = statRawName;
        Value = value;
        Min = min;
        Max = max;
        
        CalculationType = Enum.TryParse(calculationType, out CalculationType result) ? result : CalculationType.None;
        ApplyTargetType = Enum.TryParse(applyTargetType, out ApplyTargetType result2) ? result2 : ApplyTargetType.None;
        DataUnitType = Enum.TryParse(dataUnitType, out DataUnitType result3) ? result3 : DataUnitType.None;
    }   
    
    public void AddValue(float value)
    {
        Value += value;
        if (Value > Max) Value = Max;
        if (Value < Min) Value = Min;
    }
    public void InitValue()
    {
        Value = 0;
    }
    
}