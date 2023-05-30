using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] int Currency;

    public static event Action<int> OnCurrencyChanged;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnCurrencyChanged.Invoke(Currency);
    }

    public void Pay(int amount)
    {
        Currency -= amount;
        OnCurrencyChanged.Invoke(Currency);
    }

    [ContextMenu("Give Me Money")]
    public void GetCurrency()
    {
        Currency += 100;
        OnCurrencyChanged.Invoke(Currency);
    }

    public bool CanAfford(int amount)
    {
        return Currency >= amount;
    }

    public void ObtainCurrency(int amount)
    {
        Currency += amount;
        OnCurrencyChanged.Invoke(Currency);
    }
}
