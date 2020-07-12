using UnityEngine;
using UnityEditor;
using Zenject;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent waveUnleashed;
    [Inject][SerializeField]
    Defender[] defenders;
    [Inject]
    Fortress fortress;
    [Inject]
    GameConfig gc;
    [Inject]
    private EnemyController ec;
    [Inject]
    private UIController uiController;

    public int WaveNumber { get; private set; } = 0;
    public int Kills { get; private set; } = 0;
    private void Start()
    {
        Debug.Log("My Code Begins here");
        Play();
    }
    public void Play()
    {
        Restart();
    }
    public void GameOver()
    {
        CancelInvoke();
        ec.ResetEnemyController();
        print("gameOver");
    }
    public void Exit()
    {
    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
    public void Restart()
    {
        CancelInvoke();
        WaveNumber = 0;
        Kills = 0;
        ec.ResetEnemyController();
        InvokeRepeating("CreateRecurringWave", gc.EnemyUnleashDelay, gc.WaveCooldown);
    }

    void Update()
    {
        ec.UpdateOffenders();      
    }
    private void CreateRecurringWave()
    {
        WaveNumber++;
        ec.CreateWave(WaveNumber);
        waveUnleashed.Invoke();
    }
    public void EnemyKilled(int bounty)
    {        
        Kills++;
        fortress.AddGold(bounty);
        uiController.OnEnemyKilled(Kills);
    }
}
