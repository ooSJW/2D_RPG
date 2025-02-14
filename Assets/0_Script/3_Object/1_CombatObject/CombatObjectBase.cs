using UnityEngine;

public partial class CombatObjectBase : MonoBehaviour // Data Field
{
    private int totalDamage;

    protected int hp;
    public int Hp { get => hp; protected set => hp = value; }
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
    public void SendDamage(int damaga, CombatObjectBase targetObject)
    {
        targetObject.ReceiveDamage(damaga);
    }

    public void ReceiveDamage(int damage)
    {
        if (Hp - damage > 0)
            Hp -= damage;
        else
            Hp = 0;

        print($"[{name}]ReceiveDamage : {damage}");
    }
}