using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    private const string GearsTextFormat = "Gears:{0}";
    private const string HPTextFormat = "Lives:{0}";

    [SerializeField] TextMeshProUGUI GearsText;
    [SerializeField] TextMeshProUGUI HeroHP;


    private void Awake()
    {
        Inventory.OnCurrencyChanged += UpdateScoreText;
        MainHero.OnHeroDamaged += UpdateHeroHP;
    }
    private void Start()
    {

    }

    void UpdateHeroHP(int HP)
    {
        HeroHP.text = string.Format(HPTextFormat, HP.ToString());
        Debug.Log(HP);
    }

    void UpdateScoreText(int score)
    {
        GearsText.text = string.Format(GearsTextFormat, score.ToString());
        //GearsText.text = score.ToString();
    }
}
