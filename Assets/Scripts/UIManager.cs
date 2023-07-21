using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    private const string GearsTextFormat = "Gears:{0}";
    private const string HPTextFormat = "Lives:{0}";

    [Header("In Game")]
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject VolumeOn;
    [SerializeField] GameObject VolumeOff;

    GameObject HealthBar;
    [SerializeField] GameObject FullHealthBar;
    [SerializeField] GameObject HighHealthBar;
    [SerializeField] GameObject HalfHealthBar;
    [SerializeField] GameObject LowHealthBar;
    [SerializeField] GameObject EmptyHealthBar;

    GameObject TurretBar;
    [SerializeField] GameObject FullTurretBar;
    [SerializeField] GameObject HighTurretBar;
    [SerializeField] GameObject HalfTurretBar;
    [SerializeField] GameObject LowTurretBar;
    [SerializeField] GameObject EmptyTurretBar;

    [SerializeField] GameObject WaveBar;


    private void Awake()
    {
        Inventory.OnCurrencyChanged += UpdateCurrency;
        MainHero.OnHeroDamaged += UpdateHeroHP;
    }
    private void Start()
    {
        HealthBar = FullHealthBar;
        TurretBar = FullTurretBar;
    }

    #region ingameUI










    void UpdateHeroHP(int HP)
    {
        HealthBar.SetActive(false);
        switch (HP)
        {
            case 0:
                EmptyHealthBar.SetActive(true);
                HealthBar = EmptyHealthBar;
                break;
            case 1:
                LowHealthBar.SetActive(true);
                HealthBar = LowHealthBar;
                break;
            case 2:
                HalfHealthBar.SetActive(true);
                HealthBar = HalfHealthBar;
                break;
            case 3:
                HighHealthBar.SetActive(true);
                HealthBar = HighHealthBar;
                break;
            case 4:
                FullHealthBar.SetActive(true);
                HealthBar = FullHealthBar;
                break;
            default:
                break;
        }
    }

    void UpdateCurrency(int gears)
    {
        TurretBar.SetActive(false);
        switch (gears)
        {
            case < 25:
                EmptyTurretBar.SetActive(true);
                TurretBar = EmptyTurretBar;
                break;
            case < 50:
                LowTurretBar.SetActive(true);
                TurretBar = LowTurretBar;
                break;
            case < 75:
                HalfTurretBar.SetActive(true);
                TurretBar = HalfTurretBar;
                break;
            case < 100:
                HighTurretBar.SetActive(true);
                TurretBar = HighTurretBar;
                break;
            case 100:
                FullTurretBar.SetActive(true);
                TurretBar = FullTurretBar;
                break;
            default:
                break;
        }
    }
    #endregion
}
