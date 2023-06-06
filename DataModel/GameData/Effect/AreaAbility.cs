﻿[System.Serializable]
public class AreaAbility
{
    public string Name { get; private set; }
    public bool IsStackable { get; private set; }
    public int StackCount { get; private set; }
    public int MaxStackCount { get; private set; }
    public float Range { get; private set; }
    public int ApplyTargetCount { get; private set; }
    public ApplyTargetType ApplyTargetType { get; private set; }
    public float Chance { get; set; }
    public List<EffectAbilityInfo> EffectAbilities { get; private set; }
    public string Description { get; private set; }
}