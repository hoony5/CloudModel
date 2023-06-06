[System.Serializable]
public class ExpenseAbilityInfo
{
    public string EffectName { get; private set; }
    public List<ExpenseAbilityStat> ExpenseStats { get; private set; }
    
    public ExpenseAbilityInfo()
    {
        ExpenseStats = new List<ExpenseAbilityStat>(16);
    }
    
    public ExpenseAbilityInfo(string effectName, List<ExpenseAbilityStat> expenseStats)
    {
        EffectName = effectName;
        ExpenseStats = expenseStats;
    }
}
