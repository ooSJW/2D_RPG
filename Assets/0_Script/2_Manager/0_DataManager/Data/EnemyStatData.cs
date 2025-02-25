using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class EnemyStatData  // Information
{
    public class EnemyStatInformation : CombatObjectBaseInformation
    {
        public string name;
        public string ui_name;

        public float attack_delay;
    }
}

public partial class EnemyStatData  // Data Field
{
    private Dictionary<string, EnemyStatInformation> enemyStatInformationDict;
}

public partial class EnemyStatData  // Initialize
{
    private void Allocate()
    {
        enemyStatInformationDict = new Dictionary<string, EnemyStatInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<EnemyStatInformation>("EnemyStatData", enemyStatInformationDict);
    }
}
public partial class EnemyStatData  // Property
{
    public EnemyStatInformation GetData(string index)
    {
        return enemyStatInformationDict[index];
    }

    public EnemyStatInformation GetData(EnemyName enemyName)
    {
        EnemyStatInformation enemyStatInformation = enemyStatInformationDict.
            FirstOrDefault(elem => elem.Value.name == enemyName.ToString()).Value;
        return enemyStatInformation;
    }
}