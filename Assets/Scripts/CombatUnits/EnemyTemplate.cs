using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Tower Defence/EnemyTemplate")]
public class EnemyTemplate : CombatUnitTemplate, IEnemyTemplate
{
    [SerializeField]
    private EnemyStats enemyStats;
    public EnemyStats EnemyStats => enemyStats;       
}
