using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public Slider healthSlider;
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;
    public TMP_Text revivesLeft;

    public TMP_Text waveCounter;
    public TMP_Text robotCounter;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }
}
