using UnityEngine;

public class CombatObjectBaseInformation : BaseInformation
{
    public int max_hp;
    public int min_damage;
    public int max_damage;

    public float move_speed;
    public float[] attack_range;
    public float[] attack_offset;
    public float critical_percent;
    public float critical_increase;
}
