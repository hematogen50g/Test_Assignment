using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
[System.Serializable]
public class IntEvent : UnityEvent<int>
{

}
public enum OffenderStatus { Running,Attacking,TakingDamage,Dead,Pooled,BoundToWave}
[Serializable]
/// <summary>
/// Actual enemy on screen, who moves towards players base.
/// </summary>
public class Offender : MonoBehaviour
{
    public UnityEvent<int> offenderKilled = new IntEvent();
    [SerializeField]
    private IEnemyTemplate enemyTemplate;
    [SerializeField]
    private EnemyStats es;
    [SerializeField]
    private CombatUnitStats cus;
    public OffenderStatus Status { get; set; } = OffenderStatus.Pooled;    
    private EnemyController enemyController;
    private Waypoint[] waypoints;
    private GameConfig gameConfig;
        
    private GameController gameController;
    private Fortress fortress;
    
    public Transform t;
    public SpriteRenderer sr;
    private int currentWaypoint;
    private float waypointProxy;
    private Vector3[] waypointVectors;
    public void InitOffender(IEnemyTemplate et, Waypoint[] waypoints, 
        GameConfig gameConfig, EnemyController enemyController, GameController gameController, Fortress fortress)
    {

        cus = new CombatUnitStats(et.CombatUnitStats.HitPoints, et.CombatUnitStats.HitPointsIncrement,
            et.CombatUnitStats.Damage, et.CombatUnitStats.DamageIncrement, et.CombatUnitStats.AttackCooldown);
        es = new EnemyStats(et.EnemyStats.Bounty, et.EnemyStats.BountyIncrement,
            et.EnemyStats.MoveSpeed, et.EnemyStats.MoveSpeedIncrement);

        SetStatsLevel(0);
        
        this.gameConfig = gameConfig;
        this.waypoints = waypoints;
        this.enemyController = enemyController;
        this.gameController = gameController;
        this.fortress = fortress;       

        waypointProxy = gameConfig.WaypointProximity;
        currentWaypoint = 0;

        waypointVectors = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointVectors[i] = waypoints[i].Position;           
        }
             
        t = transform;
        sr = GetComponent<SpriteRenderer>();

        Status = OffenderStatus.Pooled;
        //look at first waypoint
        ResetOffender();               
    }
    public void UpdateOffender()
    {
        switch (Status)
        {
            case OffenderStatus.Pooled:
                break;
            case OffenderStatus.Running:
                t.Translate(es.MoveSpeed * Time.deltaTime, 0, 0, t);
                //calculate waypoint proximity
                float dist = Vector3.SqrMagnitude(t.position - waypointVectors[currentWaypoint]);
                if (dist < waypointProxy)
                {
                    SetNextWaypoint();
                }
                break;
            case OffenderStatus.Attacking:

                attackCooldownCounter += Time.deltaTime;
                if (attackCooldownCounter >= cus.AttackCooldown)
                {
                    attackCooldownCounter = 0;
                    Attack();
                }
                break;
            case OffenderStatus.TakingDamage:
                break;
            case OffenderStatus.Dead:
                enemyController.ResurrectMePlease(this);
                break;                      
        }                 
    }    
    
    private float attackCooldownCounter = 0;
    private void Attack()
    {       
        //enemy attacks once, and dies.
        fortress.TakeDamage(cus.Damage);
        Status = OffenderStatus.Dead;
    }
    private void SetNextWaypoint()
    {       
        if (currentWaypoint < waypointVectors.Length-1)
        {
            currentWaypoint++;
            t.right = -(t.position - waypointVectors[currentWaypoint]);
        }
        else
        {
            //if this is last waypoint it means that offender reached it`s goal. now it can attack the fortress.
            Status = OffenderStatus.Attacking;
            //reset attack cooldown
            attackCooldownCounter = cus.AttackCooldown;
        }
    }      
    public void SetStatsLevel(int level)
    {
        cus.SetLevel(level);
        es.SetLevel(level);       
    } 
    public void TakeDamage(int damage)
    {
        cus.HitPoints -= damage;
        if (cus.HitPoints <= 0)
        {
            Status = OffenderStatus.Dead;
            //invoke enemy killed event
            offenderKilled.Invoke(es.Bounty);
        }
    }
    public void ResetOffender()
    {
        SetStatsLevel(0); // set stats to zero level
        currentWaypoint = 0;
        Status = OffenderStatus.Pooled;
        t.localPosition = Vector3.zero;
        t.right = -(t.position - waypointVectors[currentWaypoint]);
    }
}

