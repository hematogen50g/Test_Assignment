using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class Fortress : MonoBehaviour
{
    public UnityEvent fortressTakeDamage;
    public UnityEvent fortressDestroyed;
    public float HitPointsPercentage { get { return (float)hp / (float)maxHp; } }

    [SerializeField]
    private int hp,maxHp;
    [SerializeField]
    private int gold,startGold;
    public int Gold { get { return gold; } }
    public void AddGold(int amount)
    {
        gold += amount;
    }
    public void SpendGold(int amount)
    {
        if(gold-amount > 0)
            gold -= amount;
    }
    [Inject]
    public void Init(GameConfig gc,GameController gameController)
    {
        fortressDestroyed.AddListener(gameController.GameOver);
        startGold = gc.StartGold;
        maxHp = gc.FortressMaxHP;
        ResetFortress();
    }
    public void ResetFortress()
    {
        gold = startGold;
        hp = maxHp; 
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

        fortressTakeDamage.Invoke();
        if (hp < 0)
        {
            fortressDestroyed.Invoke();
        }
    }
}
