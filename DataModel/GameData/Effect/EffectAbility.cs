[System.Serializable]
public class EffectAbility
{
    public string EffectName { get; private set; }
    public List<EffectAbilityInfo> AbilityInfos { get; private set; }

    public EffectAbility(string effectName, List<EffectAbilityInfo> abilityInfos)
    {
        EffectName = effectName;
        AbilityInfos = abilityInfos;
    }
}