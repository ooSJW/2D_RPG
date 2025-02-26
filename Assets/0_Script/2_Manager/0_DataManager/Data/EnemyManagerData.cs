using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class EnemyManagerData  // Information
{
    public class EnemyManagerInformation : BaseInformation
    {
        public string scene_name;
        public int max_spawn_count;
        public float spawn_time;
        public string[] spawnable_enemy;
    }
}
public partial class EnemyManagerData // Data Field
{
    private Dictionary<string, EnemyManagerInformation> enemyManagerInformationDict;
}
public partial class EnemyManagerData // Initialize
{
    private void Allocate()
    {
        enemyManagerInformationDict = new Dictionary<string, EnemyManagerInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<EnemyManagerInformation>("EnemyManagerData", enemyManagerInformationDict);
    }
}
public partial class EnemyManagerData  // Property
{
    public EnemyManagerInformation GetData(string index)
    {
        return enemyManagerInformationDict[index];
    }

    public EnemyManagerInformation GetData(SceneName combatSceneName)
    {
        EnemyManagerInformation info = enemyManagerInformationDict.
            FirstOrDefault(elem => elem.Value.scene_name == combatSceneName.ToString()).Value;
        return info;
    }
}