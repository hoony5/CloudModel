[System.Serializable]
public class Why
{
    public string Name { get; private set; }
    public string[] TriggerConditions { get; private set; }
    public string[] RewardsEachCount { get; private set; }
    public string[] Rewards { get; private set; }
    public string[] PunishmentEachCount { get; private set; }
    public string[] Punishments { get; private set; }
    
    public Why(string name, string[] triggerConditions, string[] rewardsEachCount, string[] rewards, string[] punishmentEachCount, string[] punishments)
    {
        Name = name;
        TriggerConditions = triggerConditions;
        RewardsEachCount = rewardsEachCount;
        Rewards = rewards;
        PunishmentEachCount = punishmentEachCount;
        Punishments = punishments;
    }
}
