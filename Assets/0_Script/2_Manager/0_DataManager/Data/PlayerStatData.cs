using System.Collections.Generic;
using UnityEngine;

public partial class PlayerStatData  // Information
{
    [System.Serializable]
    public class PlayerStatInformation : BaseInformation
    {
        public int max_hp;
        public int max_exp;
        public int max_level;
        public int min_damage;
        public int max_damage;
        public float critical_percent;
        public float critical_increase;
    }
}
public partial class PlayerStatData // Data Field
{
    private Dictionary<string, PlayerStatInformation> playerStatInformationDict;
}
public partial class PlayerStatData  // Initialize
{
    private void Allocate()
    {
        playerStatInformationDict = new Dictionary<string, PlayerStatInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<PlayerStatInformation>("PlayerStatData", playerStatInformationDict);
    }
}
public partial class PlayerStatData  // 
{
    public PlayerStatInformation GetData(string index)
    {
        return playerStatInformationDict[index];
    }
}