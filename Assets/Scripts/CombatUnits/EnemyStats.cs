using System;
using UnityEngine;
[System.Serializable]
public class EnemyStats
{   
    [SerializeField]
    private int
        bounty = 5,
        bountyIncrement = 3;

    [SerializeField]
    private float
        moveSpeed = 2f,
        moveSpeedIncrement = 0.1f;

    private int bountyBase;
    private float moveSpeedBase;
    public int BountyIncrement => bountyIncrement;
    public float MoveSpeedIncrement => moveSpeedIncrement;
    public int Bounty => bounty;
    public float MoveSpeed => moveSpeed;

    public void SetLevel(int level)
    {
        bounty = bountyBase + bountyIncrement * level;
        moveSpeed = moveSpeedBase + moveSpeedIncrement * level;
    }

    public EnemyStats(int bounty, int bountyIncrement, float moveSpeed, float moveSpeedIncrement)
    {
        this.bounty = bountyBase = bounty;
        this.bountyIncrement = bountyIncrement;
        this.moveSpeed = moveSpeedBase = moveSpeed;
        this.moveSpeedIncrement = moveSpeedIncrement;
    }
}
