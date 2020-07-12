using System;
using UnityEngine;
[Serializable]
/// <summary>
/// Stats are needed for runtime usage, they can have Level Ups and upgrades.
/// </summary>
public class CombatUnitStats
{
    [SerializeField]
    private int
        hitPoints = 10,
        hitPointsIncrement = 1,
        damage = 5,
        damageIncrement = 1;

    [SerializeField]
    private float
        attackCooldown = 2f;

    private int hpBase,damageBase;
    private float attackCooldownBase;
    
    public int HitPointsIncrement => hitPointsIncrement;
    public int DamageIncrement => damageIncrement;

    public int Damage { get { return damage; } }
    public int HitPoints { get { return hitPoints; } set { hitPoints = value; } }
    public float AttackCooldown { get { return attackCooldown; } }

    public void SetLevel(int level)
    {
        hitPoints = hpBase; // maybe should not be here?
        hitPoints += (hitPointsIncrement*level);
        damage += (damageIncrement*level);
    }

    public CombatUnitStats(int hitPoints, int hitPointsIncrement, int damage, int damageIncrement, float attackCooldown)
    {
        this.hitPoints = hpBase = hitPoints;
        this.hitPointsIncrement = hitPointsIncrement;
        this.damage = damageBase = damage;
        this.damageIncrement = damageIncrement;
        this.attackCooldown = attackCooldownBase = attackCooldown;
        
    }
}
