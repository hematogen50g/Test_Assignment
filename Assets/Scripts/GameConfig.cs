using UnityEngine;

[CreateAssetMenu (fileName ="Game Config",menuName = "Tower Defence/Game config")]
[System.Serializable]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private int
        startGold = 10,
        fortressMaxHP = 150,
        extraEnemiesMax = 5,
        enemyPoolSize = 100;
    [SerializeField]
    private float
        waveCooldown = 9.5f,
        enemyUnleashDelay = 0.75f,
        waypointProximity = 0.5f;
    [SerializeField]
    private bool
        showWaypoints = true;

    public int StartGold { get { return startGold; } }
    public int FortressMaxHP { get { return fortressMaxHP; } }
    public int ExtraEnemiesMax { get { return extraEnemiesMax; } }
    public int EnemyPoolSize { get { return enemyPoolSize; } }
    public float WaveCooldown { get { return waveCooldown; } }
    public float EnemyUnleashDelay { get { return enemyUnleashDelay; } }
    public float WaypointProximity { get { return waypointProximity; } }

    public bool ShowWaypoints { get { return showWaypoints; } }
}




