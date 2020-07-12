using UnityEngine;
[System.Serializable]
public abstract class CombatUnitTemplate : ScriptableObject, ICombatUnitTemplate
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField]
    private CombatUnitStats combatUnitStats;
    public CombatUnitStats CombatUnitStats =>combatUnitStats;     
}
