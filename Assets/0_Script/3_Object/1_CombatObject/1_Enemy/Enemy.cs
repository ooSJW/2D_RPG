using UnityEngine;

public partial class Enemy : CombatObjectBase // Data Field
{

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
    }
    private void Setup()
    {

    }
}
public partial class Enemy : CombatObjectBase // Property
{

}