[System.Serializable]
public class BattleBehaviour
{
    public string Name {get; private set;}
    public string[] Effects{get; private set;}
    public Rank Rank {get; private set;}
    public Rank MaxRank {get; private set;}
    public Grade Grade {get; private set;}
    public BattleFormulaInfo FormulaInfo{get; private set;}
    public BehaviourValueInfo BehaviourValueInfo{get; private set;}
    public ExpenseAbilityInfo ExpenseAbilityInfos{get; private set;}
    public BehaviourReferenceInfo BehaviourReferenceInfo{get; private set;}

    public BattleBehaviour(string name, string[] effects, string grade, string rank, string maxRank)
    {
        Name = name;
        Effects = effects;
        Rank = (Rank)System.Enum.Parse(typeof(Rank), rank);
        MaxRank = (Rank)System.Enum.Parse(typeof(Rank), maxRank);
        Grade = (Grade)System.Enum.Parse(typeof(Grade), grade);
    }
}