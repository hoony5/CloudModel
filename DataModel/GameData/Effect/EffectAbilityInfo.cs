[System.Serializable]
public class EffectAbilityInfo
{
    public string AbilityName { get; private set; }
    public List<EffectAbilityStat> AbilityStats { get; private set; }

    public EffectAbilityInfo(string abilityName)
    {
        AbilityName = abilityName;
        if (abilityName == "Empty") return;
        AbilityStats = new List<EffectAbilityStat>(16);
    }
}