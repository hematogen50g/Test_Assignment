using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Defender", menuName = "Tower Defence/Defender Data")]
public class DefenderData : ScriptableObject
{
    [SerializeField]
    private int
        damage = 5,
        damageIncrement = 1,
        upgradeCost = 20,
        upgradeCostIncrement = 5;
    [SerializeField]
    private float
        reloadTime = 3f,
        reloadTimeDecrement = 0.1f,
        reloadRateMin = 0.2f,
        fireDuration = 0.2f;

    public int Damage { get { return damage; } }
    public int DamageIncrement { get { return damageIncrement; } }
    public int UpgradeCostBase { get { return upgradeCost; } }
    public int UpgradeCostIncrement { get { return upgradeCostIncrement; } }
    public float ReloadTime { get { return reloadTime; } }
    public float ReloadTimeDecrement { get { return reloadTimeDecrement; } }
    public float ReloadRateMin { get { return reloadRateMin; } }
    public float FireDuration { get { return fireDuration; } }

    public DefenderData GetData()
    {
        DefenderData d = new DefenderData();
        d.damage = damage;
        d.damageIncrement = damageIncrement;
        d.upgradeCost = upgradeCost;
        d.upgradeCostIncrement = upgradeCostIncrement;
        d.reloadTime = reloadTime;
        d.reloadTimeDecrement = reloadTimeDecrement;
        d.reloadRateMin = reloadRateMin;
        return d;
    }
}

