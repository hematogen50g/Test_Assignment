using UnityEngine;
public interface ICombatUnitTemplate
{
    GameObject Prefab { get; }
    CombatUnitStats CombatUnitStats { get; }
}