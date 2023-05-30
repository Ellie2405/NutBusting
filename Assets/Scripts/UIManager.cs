using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const string GearsTextFormat = "Gears:{0}";

    [SerializeField] TextMeshProUGUI GearsText;


    private void Awake()
    {
        Inventory.OnCurrencyChanged += UpdateScoreText;
    }
    private void Start()
    {
        
    }

    void UpdateScoreText(int score)
    {
        GearsText.text = string.Format(GearsTextFormat, score.ToString());
        //GearsText.text = score.ToString();
    }
}
