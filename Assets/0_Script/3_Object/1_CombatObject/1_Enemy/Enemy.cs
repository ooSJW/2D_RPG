using UnityEngine;

public partial class Enemy : CombatObjectBase // Data Field
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyCombat EnemyCombat { get; private set; }
    [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; }
}
public partial class Enemy : CombatObjectBase // Initialize
{
    private void Allocate()
    {

    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
        EnemyMovement.Initialize(this);
        EnemyCombat.Initialize(this);
        EnemyAnimation.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Enemy : CombatObjectBase // Property
{

}