[System.Serializable]
public class FormulaStat
{
    public string FormulaName { get; private set; }
    public string StatusName { get; private set; }
    public ReflectStatTarget ReflectStatTarget { get; private set; }
    public float ReflectValue { get; private set; }
    public DataUnitType DataUnitType { get; private set; }
    private float[] BaseValues { get; set; }
    public int Level { get; private set; }
    public int MaxLevel { get; private set; }
    public float CalculatedValue { get; private set; }
}
