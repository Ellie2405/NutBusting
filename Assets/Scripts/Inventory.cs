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
        ObtainCurrency(100);
    }

    public bool CanAfford(int amount)
    {
        return Currency >= amount;
    }

    public void ObtainCurrency(int amount)
    {
        Currency += amount;
        if (Currency > Constants.MAX_CURRENCY) Currency = Constants.MAX_CURRENCY;
        OnCurrencyChanged.Invoke(Currency);
    }
}
