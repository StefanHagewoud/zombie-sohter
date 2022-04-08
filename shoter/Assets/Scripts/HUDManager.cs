using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("IN-GAME HUD")]
    public GameObject TopBar;
    public GameObject BottomBar;

    public Slider healthSlider;
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;

    [Header("Robot Counters")]
    public TMP_Text waveCounter;
    public TMP_Text robotCounter;
    public TMP_Text waveCountDown;
    public GameObject waveCountDownObject;

    public TMP_Text respawnsCounter;

    [Header("Game-Over")]
    public GameObject gameOverScreen;
    public TMP_Text finalCountDown;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        TopBar.SetActive(false);
        BottomBar.SetActive(false);
    }
}
