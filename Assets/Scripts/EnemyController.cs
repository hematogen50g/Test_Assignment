using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class EnemyController : MonoBehaviour
{
    private Transform enemyParent;  
    [SerializeField]
    private IEnemyTemplate[] enemyTemplates;
    /// <summary>
    /// Pool of slain offenders. They await resurrection and worship great Random to be chosen.
    /// </summary>
    private List<Offender> offendersPool;
    /// <summary>
    /// Wave of offenders.They await your command.
    /// </summary>
    private List<Offender> wave;   
    /// <summary>
    /// They are running to player`s fortress to destroy it.
    /// </summary>
    private List<Offender> activeOffenders;
    [SerializeField]
    [Inject]
    private Waypoint[] waypoints;
    [Inject]
    private GameConfig gameConfig;
    [Inject]
    private GameController gameController;
    [Inject]
    private Fortress fortress;
    public void OnValidate()
    {
        enemyParent = transform;                     
    }
    public void ResetEnemyController()
    {
        //also reset enemy stats
        CancelInvoke();
        wave.Clear();
        activeOffenders.Clear();
       
        foreach (var item in offendersPool)
        {
            item.ResetOffender();
        }
    }
    [Inject]
    public void Init(IEnemyTemplate[] enemyTemplates)
    {
        //get reference to original ScriptableObject
        this.enemyTemplates = enemyTemplates;       
        //init offenders
        activeOffenders = new List<Offender>();
        wave = new List<Offender>();        
        PoolOffenders();        
    }
    public void PoolOffenders()
    {
        Offender o;        
        offendersPool = new List<Offender>();
        //create pool for every type of enemy.
        for (int i = 0; i < enemyTemplates.Length; i++)
        {            
            for (int j = 0; j < gameConfig.EnemyPoolSize; j++)
            {
                GameObject go = Instantiate(enemyTemplates[i].Prefab, enemyParent);
                o = go.GetComponent<Offender>();               
                o.InitOffender(enemyTemplates[i], waypoints, gameConfig, this, gameController, fortress);
                offendersPool.Add(o);

                // add event listeners                
                o.offenderKilled.AddListener(gameController.EnemyKilled);             
            }
        }
    }
    public void CreateWave(int waveNumber)
    {
        int offendersCount = gameConfig.ExtraEnemiesMax + waveNumber;
        //Level of upgrades
        int level = waveNumber - 1;

        //in case offenders pool does not have enough enemies to create wave
        if (activeOffenders.Count + offendersCount >= offendersPool.Count)
        {
            Debug.Log("Offenders pool is depleted");
            return;
        }
      
        Offender o;
       
        while (offendersCount > 0)
        {
            //get random enemy from pool
            o = offendersPool[Random.Range(0, offendersPool.Count)];
            if (o.Status == OffenderStatus.Pooled)
            {
                o.SetStatsLevel(level);
                o.Status = OffenderStatus.BoundToWave;
                wave.Add(o);
                offendersCount--;
            }
        }
        UnleashWave(gameConfig.EnemyUnleashDelay);
    }
    /// <summary>
    /// Send created wave to battle
    /// </summary>
    /// <param name="cooldown">cooldown between offenders</param>
    public void UnleashWave(float cooldown)
    {
        unleashCooldown = cooldown;
        Invoke("Unleash", unleashCooldown);
    }
    private float unleashCooldown;
    private void Unleash()
    {
        if (wave.Count > 0) 
        {
            //print("Unleashed");
            wave[0].Status = OffenderStatus.Running;
            activeOffenders.Add(wave[0]);          
            wave.RemoveAt(0);
            Invoke("Unleash", unleashCooldown); 
        }       
    }   
    public void ResurrectMePlease(Offender o)
    {
        o.ResetOffender();
        activeOffenders.Remove(o);                       
    }
    public void UpdateOffenders()
    {
        for (int i = 0; i < activeOffenders.Count; i++)
        {
            activeOffenders[i].UpdateOffender();
        }        
    }
}
