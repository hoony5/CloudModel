[System.Serializable]
public class BattleFormulaInfo
{
    public string Name { get; private set; }
    public bool UseClampValue { get; private set; }
    public List<FormulaStat> FormulaStats { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }
    public string Description { get; private set; }
}