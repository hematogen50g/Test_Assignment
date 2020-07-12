using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

class UIController:MonoBehaviour
{
    [SerializeField]
    private Text killsText, waveText,goldText,gameOverKills,gameOverWave;
    private Image fortressHPImage;
    [SerializeField]
    private Canvas menuCanvas, gameplayCanvas,gameOverCanvas;  
    private GameController gameController;  
    private Fortress fortress;
    private string killsPrefix, wavePrefix,goldPrefix,gameOverWavePrefix,gameOverKillsPrefix;
    [Inject]
    public void Init(Fortress fortress,GameController gameController)
    {
        fortress.fortressTakeDamage.AddListener(OnFortressTakeDamage);
        fortress.fortressDestroyed.AddListener(OnFortressDestroyed);
        this.fortress = fortress;
        this.gameController = gameController;
    }
    public void Start()
    {
        killsPrefix = "Kills: ";
        wavePrefix = "Wave: ";
        goldPrefix = "Gold: ";
        gameOverWavePrefix = "Waves survived: ";
        gameOverKillsPrefix = "Total kills ";
        ResetUI();
        Debug.Log(gameController);
    }

    private void OnValidate()
    {
        menuCanvas = transform.Find("MenuCanvas").GetComponent<Canvas>();
        gameplayCanvas = transform.Find("GameplayCanvas").GetComponent<Canvas>();
        gameOverCanvas = transform.Find("GameOverCanvas").GetComponent<Canvas>();

        //find gameplay texts
        killsText = GameObject.Find("KillsText").GetComponent<Text>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        waveText = GameObject.Find("WaveNumberText").GetComponent<Text>();
        fortressHPImage = GameObject.Find("FortressHP").GetComponent<Image>();

        // find kills ond wave texts for gameover.
        gameOverKills = gameOverCanvas.transform.Find("VerticalLayout").Find("Kills").Find("Text").GetComponent<Text>();
        gameOverWave = gameOverCanvas.transform.Find("VerticalLayout").Find("Waves").Find("Text").GetComponent<Text>();
        //find buttons
        Button play, exit1,exit2, restart1,restart2;
        play = menuCanvas.transform.Find("VerticalLayout").Find("Play").GetComponent<Button>();
        exit1 = menuCanvas.transform.Find("VerticalLayout").Find("Exit").GetComponent<Button>();
        exit2 = gameOverCanvas.transform.Find("VerticalLayout").Find("Exit").GetComponent<Button>();
        restart1 = gameOverCanvas.transform.Find("VerticalLayout").Find("Restart").GetComponent<Button>();
        restart2 = gameplayCanvas.transform.Find("Restart").GetComponent<Button>();

        //Add event listeners
        play.onClick.AddListener(OnPlayButtonClick);
        exit1.onClick.AddListener(OnExitButtonClick);
        exit2.onClick.AddListener(OnExitButtonClick);
        restart1.onClick.AddListener(OnRestartButtonClick);
        restart2.onClick.AddListener(OnRestartButtonClick);
    }

    public void ShowMenuCanvas(bool show)
    {
        menuCanvas.enabled = show;
    }
    public void ShowGameCanvas(bool show)
    {
        gameplayCanvas.enabled = show;
    }
    public void ShowGameOverCanvas(bool show)
    {
        gameOverCanvas.enabled = show;
    }

    public void OnExitButtonClick()
    {
        gameController.Exit();
    }
    public void OnRestartButtonClick()
    {
        ShowGameOverCanvas(false);
        gameController.Restart();
        ResetUI();
    }
    private void ResetUI()
    {
        killsText.text = killsPrefix + "0";
        waveText.text = wavePrefix + "0";
        goldText.text = goldPrefix + "0";
        fortressHPImage.fillAmount = 1;
    }
    public void OnPlayButtonClick()
    {
        gameController.Play();
        ShowMenuCanvas(false);       
    }
    public void OnEnemyKilled(int totalKills)
    {
        killsText.text = killsPrefix + totalKills.ToString();
        goldText.text = goldPrefix + fortress.Gold.ToString();
    }
    public void OnFortressTakeDamage()
    {
        fortressHPImage.fillAmount = fortress.HitPointsPercentage;
    }
    public void OnFortressDestroyed()
    {
        ShowGameOverCanvas(true);
        gameOverKills.text = gameOverKillsPrefix + gameController.WaveNumber;
        gameOverWave.text = gameOverWavePrefix + gameController.Kills;
    }
    public void OnWaveUnleashed()
    {
        waveText.text = wavePrefix + gameController.WaveNumber;
    }
}



