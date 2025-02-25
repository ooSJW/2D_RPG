using UnityEngine;

public partial class CombatObjectBase : MonoBehaviour // Data Field
{
    private int totalDamage;

    protected int hp;
    public virtual int Hp { get => hp; protected set => hp = value; }
}
public partial class CombatObjectBase : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public virtual void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class CombatObjectBase : MonoBehaviour // Property
{
    public void SendDamage(CombatObjectBaseInformation sender, CombatObjectBase targetObject)
    {
        int minDamage = sender.min_damage;
        int maxDamage = sender.max_damage;
        bool isCritical = false;
        float resultDamage = Random.Range(minDamage, maxDamage + 1);
        float critical = Random.Range(0f, 1f);

        if (critical < sender.critical_percent)
        {
            resultDamage += resultDamage * sender.critical_increase;
            isCritical = true;
        }
        targetObject.ReceiveDamage((int)resultDamage, isCritical);
    }

    public void ReceiveDamage(int damage, bool isCritical = false)
    {
        if (Hp - damage > 0)
            Hp -= damage;
        else
            Hp = 0;

        print($"[{name}]ReceiveDamage : {damage} , Critical : {isCritical}");
    }
}